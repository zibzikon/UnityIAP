using System;
using UnityEngine;
using UnityEngine.UI;

namespace Demos.Demo_1.Kernel.UI
{
    public class DataSavingButton : MonoBehaviour
    {
        [SerializeField] private Button _button;  
        private IGameDataSaver _gameDataSaver;

        private void Awake()
        {
            _button.onClick.AddListener(SaveData);
        }

        public void Initialize(IGameDataSaver gameDataSaver)
        {
            _gameDataSaver = gameDataSaver;
        }
        
        private async void SaveData()
        {
            await _gameDataSaver.SaveAsync();
        }
    }
}