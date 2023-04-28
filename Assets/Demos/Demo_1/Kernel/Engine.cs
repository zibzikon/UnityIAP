using System.Threading.Tasks;
using Demos.Demo_1.Kernel.Test;
using Kernel;
using Kernel.Interfaces;
using Kernel.UI;
using UnityEngine;

namespace Demos.Demo_1.Kernel
{
    public class Engine : MonoBehaviour
    {
        [SerializeField] private IAPStoreUIElementsCreator iapStoreUIElementsCreator;
        [SerializeField] private IAPStoreUIElementsInitializer iapStoreUIElementsInitializer;
        
        private IGameDataSaveLoadService _gameDataSaveLoadService;
        private IIAPStore _iapStore;

        public void Initialize(IGameDataSaveLoadService gameDataSaveLoadService, IIAPStore iapStore)
        {
            _iapStore = iapStore;
            _gameDataSaveLoadService = gameDataSaveLoadService;
        }

        public Task LoadAsync()
        {
            iapStoreUIElementsCreator.Initialize(_iapStore);
            iapStoreUIElementsInitializer.Initialize(_iapStore);
            
            return _gameDataSaveLoadService.LoadAsync();
        }

        private void SaveData()
        {
            
        }
     }
}