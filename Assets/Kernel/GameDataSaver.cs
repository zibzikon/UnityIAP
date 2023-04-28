using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kernel;

namespace Demos.Demo_1.Kernel
{
    public class GameDataSaver : IGameDataSaver
    {
        private readonly ISaveClient _saveClient;
        private readonly List<ISavingDataProvider> _dataProviders;

        public GameDataSaver(ISaveClient saveClient, IEnumerable<ISavingDataProvider> dataProviders)
        {
            _saveClient = saveClient;
            _dataProviders = dataProviders.ToList();
        }
        
        public Task SaveAsync() => _saveClient.SaveAsync(_dataProviders.Select(x => (x.Key, x.GetData())).ToArray());
        
        public void AddSavingDataProvider(ISavingDataProvider savingDataProvider) => _dataProviders.Add(savingDataProvider);

        public void RemoveSavingDataProvider(ISavingDataProvider savingDataProvider) => _dataProviders.Remove(savingDataProvider);

    }
}