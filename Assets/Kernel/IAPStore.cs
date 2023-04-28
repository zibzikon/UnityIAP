using System;
using System.Collections.Generic;
using System.Linq;
using Kernel.Interfaces;
using UnityEngine;
using UnityEngine.Purchasing;
using static UnityEngine.Purchasing.PurchaseProcessingResult;

namespace Kernel
{
    public class IAPStore : MonoBehaviour, IIAPStore, IStoreListener
    {
        public event Action<Product, PurchaseFailureReason> PurchaseFailed;
        public event Action<Product> PurchaseProcessed;
        
        public event Action<bool, string> TransactionsRestored;
        
        private Dictionary<string, List<IIAPProductProcessor>> _purchaseProcessors;
        
        private IStoreController _controller;
        private IExtensionProvider _extensions;
        private ILogger _logger;

        public void Initialize(IEnumerable<IIAPProductProcessor> productPurchaseProcessors, ILogger logger)
        {
            _logger = logger;
            _purchaseProcessors = new Dictionary<string, List<IIAPProductProcessor>>(); 
            
            foreach (var processor in productPurchaseProcessors) AddProcessor(processor);
        }

        public void Purchase(string productId) => _controller.InitiatePurchase(productId);

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _extensions = extensions;
            _controller = controller;
        }

        public void OnInitializeFailed(InitializationFailureReason error) => OnInitializeFailed(error, "");

        public void OnInitializeFailed(InitializationFailureReason error, string message) 
            => _logger.LogError($"Error initializing IAP because of {error}. {message}");

        public void RestoreTransactions()
        {
            if (Application.platform == RuntimePlatform.WSAPlayerX86 ||
                Application.platform == RuntimePlatform.WSAPlayerX64 ||
                Application.platform == RuntimePlatform.WSAPlayerARM)
            {
                _extensions.GetExtension<IMicrosoftExtensions>().RestoreTransactions();
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer ||
                     Application.platform == RuntimePlatform.OSXPlayer ||
                     Application.platform == RuntimePlatform.tvOS)
            {
                CodelessIAPStoreListener.Instance.GetStoreExtensions<IAppleExtensions>()
                    .RestoreTransactions(OnTransactionsRestored);
            }
            else if (Application.platform == RuntimePlatform.Android &&
                     StandardPurchasingModule.Instance().appStore == AppStore.GooglePlay)
            {
                CodelessIAPStoreListener.Instance.GetStoreExtensions<IGooglePlayStoreExtensions>()
                    .RestoreTransactions(OnTransactionsRestored);
            }
            else
            {
                _logger.LogWarning(Application.platform + " is not a supported platform");
            }
        }
        
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            var product = purchaseEvent.purchasedProduct;
            
            foreach (var processor in SelectProcessors(product))
            {
                var result =processor.Process(product);
                
                if (result == Pending) return Pending;
            }
            
            PurchaseProcessed?.Invoke(product);
            
            return Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            _logger.LogError($"Failed to purchase {product.definition.id} because {failureReason}");
            
            foreach (var processor in SelectProcessors(product))
                processor.OnPurchaseFailed(product, failureReason);
            
            
            PurchaseFailed?.Invoke(product, failureReason);
        }

        public void AddProcessor(IIAPProductProcessor processor)
        {
            var id = processor.ProductID;
            if (_purchaseProcessors.ContainsKey(id))
            {
                _purchaseProcessors[id].Add(processor);
            }
            
            _purchaseProcessors.Add(id, new List<IIAPProductProcessor> (){processor});
        }

        public void RemoveProcessor(IIAPProductProcessor processor)
        {
            var id = processor.ProductID;
            if (!_purchaseProcessors.ContainsKey(id)) return;
            
            var list = _purchaseProcessors[id];
            list.Remove(processor);
            if (list.Any()) _purchaseProcessors.Remove(id);
        }

        public Product GetProduct(string productId)
            => _controller.products.WithID(productId);
        
        private void OnTransactionsRestored(bool success, string error) => TransactionsRestored?.Invoke(success, error);

        private IEnumerable<IIAPProductProcessor> SelectProcessors(Product product) => _purchaseProcessors[product.definition.id];
        

    }
}