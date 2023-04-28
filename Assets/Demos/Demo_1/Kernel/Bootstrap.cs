using System.Collections.Generic;
using Demos.Demo_1.Kernel.Advertisement;
using Demos.Demo_1.Kernel.Data;
using Demos.Demo_1.Kernel.PurchaseProcessors.Atom;
using Demos.Demo_1.Kernel.Test;
using Demos.Demo_1.Kernel.UI;
using Kernel;
using Kernel.DataProviders;
using Kernel.Interfaces;
using Kernel.PurchaseProcessors;
using Kernel.UI;
using Unity.Services.CloudSave;
using UnityEngine;
using UnityEngine.Serialization;

namespace Demos.Demo_1.Kernel
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private IAPStore iapStore;
        [SerializeField] private IAPStoreUIElementsCreator iapStoreUIElementsCreator;
        [SerializeField] private IAPStoreUIElementsInitializer iapStoreUIElementsInitializer;
        [SerializeField] private List<MonoIAPProductProcessor> monoIAPProductPurchaseProcessors;
        [SerializeField] private Player player;

        private void Awake() => Boot();
        
        private async void Boot()
        {
            await UnityGamingServiceLoader.LoadAsync();
            await UserAuthenticator.SignInAnonymouslyAsync();
            
            var logger = new UnityLogger();
            var adsRunner = new AdsRunner();
            var processors = GetProcessors(adsRunner);

            var iapConfigurationBuilderFactory = new IAPConfigurationBuilderFactory();
            
            var storeLoader = new IAPStoreLoader(iapConfigurationBuilderFactory, iapStore, useFakeStore: false);
            await storeLoader.BootIAPStoreAsync();
            
            iapStore.Initialize(processors, logger);
            iapStoreUIElementsCreator.Initialize(iapStore);
            iapStoreUIElementsInitializer.Initialize(iapStore);
            player.Initialize();
        }


        private IEnumerable<IIAPProductProcessor> GetProcessors(IAdsRunner adsRunner)
        {
            foreach (var monoProcessor in monoIAPProductPurchaseProcessors) yield return monoProcessor;

            yield return new RemoveAdsProcessor(adsRunner);
        }
    }
}