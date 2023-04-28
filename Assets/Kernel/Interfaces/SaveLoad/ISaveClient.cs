using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Kernel
{
    public interface ISaveClient
    {
        Task SaveAsync(string key, object value);

        Task SaveAsync(params (string key, object value)[] values);

        Task<T> LoadAsync<T>(string key);
        Task<object> LoadAsync(Type type, string key);

        Task<IEnumerable<T>> LoadAsync<T>(params string[] keys);
        Task<IEnumerable<object>> LoadAsync(Type type, params string[] keys);
        Task<IEnumerable<string>> RetrieveAllKeysAsync();

        Task DeleteAsync(string key);

        Task DeleteAllAsync();
    }
}