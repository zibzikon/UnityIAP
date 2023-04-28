using System;
using System.Collections.Generic;
using Demos.Demo_1.Kernel.Advertisement;
using Demos.Demo_1.Kernel.PurchaseProcessors.Atom;
using Demos.Demo_1.Kernel.SaveDataProcessors;
using Kernel;
using Kernel.DataProviders;
using Kernel.Interfaces;
using Kernel.PurchaseProcessors;
using Unity.Services.CloudSave;
using UnityEngine;
using UnityEngine.Serialization;

namespace Demos.Demo_1.Kernel
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private IAPStore iapStore;
        [SerializeField] private Engine engine;
        [SerializeField] private GameSaver gameSaver;
       
        
        [SerializeField] private List<MonoIAPProductProcessor> monoIAPProductPurchaseProcessors;
        [SerializeField] private List<MonoSaveDataProcessor> monoSaveDataProcessors;

        [SerializeField] private Player player;

        private void Awake() => Boot();
        
        private async void Boot()
        {
            await UnityGamingServiceLoader.LoadAsync();
            await UserAuthenticator.SignInAnonymouslyAsync();
            
            var logger = new UnityLogger();
            var saveClient = new UnityCloudSaveClient(CloudSaveService.Instance.Data, logger);
            var adsRunner = new AdsRunner();
            var processors = GetProcessors(adsRunner);

            var gameDataSaveLoadService = new GameDataSaveLoadService(GetDataProcessors(), saveClient);
            var iapConfigurationBuilderFactory = new IAPConfigurationBuilderFactory();
            
            var storeLoader = new IAPStoreLoader(iapConfigurationBuilderFactory, iapStore, useFakeStore: false);
            await storeLoader.BootIAPStoreAsync();
            iapStore.Initialize(processors, logger);
            
            engine.Initialize(gameDataSaveLoadService, iapStore);
            await engine.LoadAsync();

            gameSaver.Initialize(gameDataSaveLoadService);
        }

        private IEnumerable<ISaveDataProcessor> GetDataProcessors()
        {
            foreach (var monoProcessor in monoSaveDataProcessors) yield return monoProcessor;
            
            yield return new PlayerSaveDataProcessor(player);
        }
        
        private IEnumerable<IIAPProductProcessor> GetProcessors(IAdsRunner adsRunner)
        {
            foreach (var monoProcessor in monoIAPProductPurchaseProcessors) yield return monoProcessor;

            yield return new RemoveAdsProcessor(adsRunner);
        }
    }
}