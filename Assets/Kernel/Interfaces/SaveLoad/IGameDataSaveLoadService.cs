using System.Threading.Tasks;

namespace Demos.Demo_1.Kernel
{
    public interface IGameDataSaveLoadService
    {
        Task SaveAsync();
        Task LoadAsync();
        void AddSavingDataProvider(ISaveDataProcessor saveDataProcessor);
        void RemoveSavingDataProvider(ISaveDataProcessor saveDataProcessor);
    }
}