using System.Threading.Tasks;
using Kernel;

namespace Demos.Demo_1.Kernel
{
    public class DataHandler<TData>
    {
        public readonly string Key;
        
        private readonly ISaveClient _saveClient;

        public DataHandler(string key, ISaveClient saveClient)
        {
            Key = key;
            _saveClient = saveClient;
        }

        public Task SaveAsync(TData data) => _saveClient.SaveAsync(Key, data);

        public Task<TData> LoadAsync() => _saveClient.LoadAsync<TData>(Key);

        public Task DeleteAsync() => _saveClient.DeleteAsync(Key);
    }
}