using System.Threading.Tasks;

namespace Demos.Demo_1.Kernel
{
    public interface IGameDataSaver
    {
        Task SaveAsync();
        void AddSavingDataProvider(ISavingDataProvider savingDataProvider);
        void RemoveSavingDataProvider(ISavingDataProvider savingDataProvider);
    }
}