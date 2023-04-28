using System;
using UnityEngine;

namespace Demos.Demo_1.Kernel
{
    public class GameSaver : MonoBehaviour
    {
        [SerializeField] private Player player;
        private IGameDataSaveLoadService _gameDataSaveLoadService;

        public void Initialize(IGameDataSaveLoadService gameDataSaveLoadService)
        {
            _gameDataSaveLoadService = gameDataSaveLoadService;
            RegisterSaveProvocationEvents();
        }

        private void OnDisable()
        {
            UnregisterSaveProvocationEvents();
        }

        private void RegisterSaveProvocationEvents()
        {
            player.Changed += Save;
        }
        
        private void UnregisterSaveProvocationEvents()
        {
            player.Changed += Save;
        }

        private void Save() => _gameDataSaveLoadService.SaveAsync();
        
    }
}