using System;
using System.Threading.Tasks;
using Kernel.Extentions;
using Kernel.Interfaces;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Kernel
{
    public class IAPStoreLoader
    {
        private const string IAPProductCatalogResourcePath = "IAPProductCatalog";
        
        private readonly IIAPConfigurationBuilderFactory _iapConfigurationBuilderFactory;
        private readonly IStoreListener _storeListener;
        private readonly bool _useFakeStore;

        public IAPStoreLoader(IIAPConfigurationBuilderFactory iapConfigurationBuilderFactory, IStoreListener storeListener, bool useFakeStore)
        {
            _iapConfigurationBuilderFactory = iapConfigurationBuilderFactory;
            _storeListener = storeListener;
            _useFakeStore = useFakeStore;
        }
    
        public async void BootIAPStore()
        {
            var catalog = await LoadIAPProductCatalogAsync();
            InitializeUnityIAP(catalog);
        }

        private async Task<ProductCatalog> LoadIAPProductCatalogAsync()
        {
            var catalogAssetRequest = await Resources.LoadAsync<TextAsset>(IAPProductCatalogResourcePath).AsTask();

            var catalogAssetJson = catalogAssetRequest.asset as TextAsset;

            if (catalogAssetJson is null) throw new InvalidOperationException();

            var catalog = JsonUtility.FromJson<ProductCatalog>(catalogAssetJson.text);
        
            return catalog;
        }

        private void InitializeUnityIAP(ProductCatalog catalog)
        {
            if (_useFakeStore)
                UseFakeStore();

            var builder = _iapConfigurationBuilderFactory.CreateBuilder(catalog);
        
            UnityPurchasing.Initialize(_storeListener, builder);
        }

        private void UseFakeStore()
        {
            StandardPurchasingModule.Instance().useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
            StandardPurchasingModule.Instance().useFakeStoreAlways = true;
        }
    }
}