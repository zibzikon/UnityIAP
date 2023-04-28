using Demos.Demo_1.Kernel;
using UnityEngine;

namespace Kernel.DataProviders
{
    public abstract class MonoSavingDataProvider : MonoBehaviour, ISavingDataProvider
    {
        public abstract string Key { get; }
        public abstract object GetData();
    }
}