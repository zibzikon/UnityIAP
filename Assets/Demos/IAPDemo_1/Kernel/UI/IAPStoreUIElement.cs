using System;
using Kernel.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace Demos.IAPDemo_1.Kernel.UI
{
    public class IAPStoreUIElement : MonoBehaviour
    {
        [Serializable] public class OnPurchaseCompletedEvent : UnityEvent<Product> { };
        [Serializable] public class OnPurchaseFailedEvent : UnityEvent<Product, PurchaseFailureReason> { };
        
        [SerializeField] private Button button;
        [SerializeField] private string productId;

        [SerializeField] private OnPurchaseCompletedEvent onPurchaseComplete;
        [SerializeField] private OnPurchaseFailedEvent onPurchaseFailed;
        
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI priceText;
        
        private IIAPStore _iapStore;

        private void OnDestroy() => UnregisterEvents();

        public void Initialize(IIAPStore iapStore)
        {
            _iapStore = iapStore;
            RegisterEvents();
            UpdateText();
        }

        private void RegisterEvents()
        {
            _iapStore.PurchaseProcessed += OnIAPStorePurchaseProcessed;
            _iapStore.PurchaseFailed += OnIAPStorePurchaseFailed;
            
            button.onClick.AddListener(OnButtonPressed);
        }
        private void UnregisterEvents()
        {
            _iapStore.PurchaseProcessed -= OnIAPStorePurchaseProcessed;
            _iapStore.PurchaseFailed -= OnIAPStorePurchaseFailed;
            button.onClick.RemoveListener(OnButtonPressed);
        }

        private void OnIAPStorePurchaseProcessed(Product product)
        {
            if (product.definition.id == productId) onPurchaseComplete.Invoke(product);
        }
        
        private void OnIAPStorePurchaseFailed(Product product, PurchaseFailureReason purchaseFailureReason)
        {
            if (product.definition.id == productId) onPurchaseFailed.Invoke(product, purchaseFailureReason);
        }

        private void OnButtonPressed()
        {
            if (_iapStore is null)
                throw new Exception(
                    $"The button: {GetType()} is not initialized when you try to access it. Please call Initialize() before accessing it");

            _iapStore.Purchase(productId);
        }
        
        private void UpdateText()
        {
            var product = _iapStore.GetProduct(productId);
            if (product != null)
            {
                var metadata = product.metadata;
                
                if (titleText != null)
                {
                    titleText.text = metadata.localizedTitle;
                }

                if (descriptionText != null)
                {
                    descriptionText.text = metadata.localizedDescription;
                }

                if (priceText != null)
                {
                    priceText.text = metadata.localizedPriceString + metadata.isoCurrencyCode;
                }
            }
        }
    }
}