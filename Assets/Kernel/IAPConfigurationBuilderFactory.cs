using Kernel.Interfaces;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Kernel
{
    public class IAPConfigurationBuilderFactory : IIAPConfigurationBuilderFactory
    {
        public ConfigurationBuilder CreateBuilder(ProductCatalog catalog)
        {
            var builder = Application.platform switch
            {
                RuntimePlatform.IPhonePlayer =>
                    ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(AppStore.AppleAppStore)),
                RuntimePlatform.Android =>
                    ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(AppStore.GooglePlay)),
            
                _ => ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(AppStore.NotSpecified)),
            };
        
            foreach (var product in catalog.allProducts)
            {
                builder.AddProduct(product.id, product.type);
            }
        
            return builder;
        }
    }
}