using System;
using Demos.Demo_1.Kernel.Data;
using UnityEngine;

namespace Demos.Demo_1.Kernel
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField ] public int Gems { get; private set; }
        
        public async void Initialize()
        {
            var data = await UnityCloudSaveClient.Instance.LoadAsync<PlayerData>("player_data");
            
            if(data !=  null)
                Gems = data.GemsCount;
        }
        
        public async void AddGems(int count)
        {
            Gems += count;
            await UnityCloudSaveClient.Instance.SaveAsync("player_data" ,new PlayerData(){GemsCount =  Gems});
        }
    }
}