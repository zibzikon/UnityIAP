using System;
using System.Threading.Tasks;
using Demos.Demo_1.Kernel;
using UnityEngine;

namespace Kernel.DataProviders
{
    public abstract class MonoSaveDataProcessor : MonoBehaviour, ISaveDataProcessor
    {
        public abstract Type Type { get; }
        public abstract string Key { get; }
        public abstract object GetData();
        public abstract Task ProcessDataLoad(object data);

    }
}