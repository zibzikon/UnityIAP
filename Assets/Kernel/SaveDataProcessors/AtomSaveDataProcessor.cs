using System;
using System.Threading.Tasks;
using Demos.Demo_1.Kernel;

namespace Kernel.DataProviders
{
    public abstract class AtomSaveDataProcessor<T> : ISaveDataProcessor<T>
    {
        public Type Type => typeof(T);
        public abstract string Key { get; }
        public abstract object GetData();
        public abstract Task ProcessDataLoadAsync(T data);
        public Task ProcessDataLoad(object data)
            => ProcessDataLoadAsync((T)data);
    }
}