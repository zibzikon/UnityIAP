using System;
using UnityEngine;
using UnityEngine.UI;

namespace Demos.Demo_1.Kernel.UI
{
    public class DataSavingButton : MonoBehaviour
    {
        [SerializeField] private Button _button;  
        private IGameDataSaveLoadService _gameDataSaveLoadService;

        private void Awake()
        {
            _button.onClick.AddListener(SaveData);
        }

        public void Initialize(IGameDataSaveLoadService gameDataSaveLoadService)
        {
            _gameDataSaveLoadService = gameDataSaveLoadService;
        }
        
        private async void SaveData()
        {
            await _gameDataSaveLoadService.SaveAsync();
        }
    }
}