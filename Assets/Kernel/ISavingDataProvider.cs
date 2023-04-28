namespace Demos.Demo_1.Kernel
{
    public interface ISavingDataProvider
    {
        string Key { get; }
        object GetData();
    }
}