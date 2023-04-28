using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demos.Demo_1.Kernel;

namespace Kernel
{
    public class GameDataSaveLoadService : IGameDataSaveLoadService
    {
        private readonly List<ISaveDataProcessor> _savingDataProcessors;
        private readonly ISaveClient _saveClient;

        public GameDataSaveLoadService(IEnumerable<ISaveDataProcessor> savingDataProviders, ISaveClient saveClient)
        {
            _savingDataProcessors = savingDataProviders.ToList();
            _saveClient = saveClient;
        }
        
        public async Task SaveAsync()
            => await _saveClient.SaveAsync(_savingDataProcessors
                    .Select(x => (x.Key, x.GetData()))
                    .ToArray());

        public async Task LoadAsync()
        {
            foreach (var savingDataProcessor in _savingDataProcessors)
            {
                var data = await _saveClient.LoadAsync(savingDataProcessor.Type, savingDataProcessor.Key);
                
                if (data != null) await savingDataProcessor.ProcessDataLoad(data);
            }
        }
           
        public void AddSavingDataProvider(ISaveDataProcessor saveDataProcessor) =>
            _savingDataProcessors.Add(saveDataProcessor);

        public void RemoveSavingDataProvider(ISaveDataProcessor saveDataProcessor) =>
            _savingDataProcessors.Remove(saveDataProcessor);

    }
}