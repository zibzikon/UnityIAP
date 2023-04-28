using System;
using System.Threading.Tasks;
using UnityEngine.iOS;

namespace Demos.Demo_1.Kernel
{
    public interface ISaveDataProcessor<in T> : ISaveDataProcessor
    {
        Task ProcessDataLoadAsync(T data);
    }
    
    public interface ISaveDataProcessor
    {
        public Type Type { get; }
        string Key { get; }
        object GetData();
        Task ProcessDataLoad(object data);
    }
}