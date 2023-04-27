using System.Collections.Generic;
using Demos.IAPDemo_1.Kernel.Advertisement;
using Demos.IAPDemo_1.Kernel.PurchaseProcessors.Atom;
using Kernel;
using Kernel.Interfaces;
using Kernel.PurchaseProcessors;
using UnityEngine;

namespace Demos.IAPDemo_1.Kernel
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private IAPStore _iapStore;
        
        [SerializeField] private List<MonoIAPProductProcessor> monoIAPProductPurchaseProcessors;

        private void Awake() => Boot();
        
        private async void Boot()
        {
            await UnityGamingServiceLoader.LoadAsync();
            
            var adsRunner = new AdsRunner();

            var processors = GetProcessors(adsRunner);
            
            var iapConfigurationBuilderFactory = new IAPConfigurationBuilderFactory();
            
            var storeLoader = new IAPStoreLoader(iapConfigurationBuilderFactory, _iapStore, useFakeStore: true);
            await storeLoader.BootIAPStoreAsync();
            
            _iapStore.Initialize(processors);
        }

        private IEnumerable<IIAPProductProcessor> GetProcessors(IAdsRunner adsRunner)
        {
            foreach (var monoProcessor in monoIAPProductPurchaseProcessors) yield return monoProcessor;

            yield return new RemoveAdsProcessor(adsRunner);
        }
    }
}