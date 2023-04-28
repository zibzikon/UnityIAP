using System;
using Demos.Demo_1.Kernel.Data;
using UnityEngine;

namespace Demos.Demo_1.Kernel
{
    public class Player : MonoBehaviour
    {
        public int Gems
        {
            get => gems;
            
            private set
            {
                gems = value;
                Changed?.Invoke();
            }
        }

        public event Action Changed;

        [SerializeField ] private int gems;
        
        public async void Initialize(int gemsCount) => Gems = gemsCount;

        public async void AddGems(int count)
        {
            Changed?.Invoke();
            Gems += count;
        }
    }
}