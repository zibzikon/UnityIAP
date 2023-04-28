using Demos.Demo_1.Kernel;

namespace Kernel.DataProviders
{
    public abstract class AtomSavingDataProvider : ISavingDataProvider
    {
        public abstract string Key { get; }
        public abstract object GetData();
    }
}