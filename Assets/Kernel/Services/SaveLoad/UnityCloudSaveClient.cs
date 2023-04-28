using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kernel;
using Unity.Services.CloudSave;
using UnityEngine;
using ILogger = Kernel.ILogger;

public class UnityCloudSaveClient : ISaveClient
{
    private readonly ICloudSaveDataClient _client;
    private readonly ILogger _logger;

    public UnityCloudSaveClient(ICloudSaveDataClient client, ILogger logger)
    {
        _client = client;
        _logger = logger;
    }
    
    public async Task SaveAsync(string key, object value)
    {
        var data = new Dictionary<string, object> { { key, value } };
        await Call(_client.ForceSaveAsync(data));
    }

    public async Task SaveAsync(params (string key, object value)[] values)
    {
        var data = values.ToDictionary(item => item.key, item => item.value);
        await Call(SaveDataAsync(data));
    }

    private Task SaveDataAsync(Dictionary<string, object> data)
    {
        return _client.ForceSaveAsync(data);
    }

    public async Task<T> LoadAsync<T>(string key) => (T) await LoadAsync(typeof(T), key);
    
    public async Task<object> LoadAsync(Type type, string key)
    {
        var query = await Call(_client.LoadAsync(new HashSet<string> { key }));
        
        return query.TryGetValue(key, out var value) ? Deserialize(type, value) : default;
    }

    public async Task<IEnumerable<T>> LoadAsync<T>(params string[] keys) =>
        (await LoadAsync(typeof(T), keys)).Select(x => (T)x);

    public async Task<IEnumerable<object>> LoadAsync(Type type, params string[] keys)
    {
        var query = await Call(_client.LoadAsync(keys.ToHashSet()));

        return keys.Select(k =>
        {
            if (query.TryGetValue(k, out var value))
            {
                return value != null ? Deserialize(type, value) : default;
            }

            return default;
        });
    }

    public async Task<IEnumerable<string>> RetrieveAllKeysAsync() => await _client.RetrieveAllKeysAsync();
    
    public async Task DeleteAsync(string key) => await Call(_client.ForceDeleteAsync(key));

    public async Task DeleteAllAsync()
    {
        var keys = await Call(_client.RetrieveAllKeysAsync());
        
        var tasks = keys.Select(DeleteAsync);
        
        await Call(Task.WhenAll(tasks));
    }

    private T Deserialize<T>(string token) => (T)Deserialize(typeof(T), token);
    
    private object Deserialize(Type type, string token)
    {
        if (type == typeof(string)) return token;
        return JsonConvert.DeserializeObject(token, type);;
    }

    private async Task Call(Task action)
    {
        try
        {
            await action;
            
        }
        catch (CloudSaveValidationException e)
        {
            _logger.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            _logger.LogError(e);
        }
        catch (CloudSaveException e)
        {
            _logger.LogError(e);
        }
    }

    private async Task<T> Call<T>(Task<T> action)
    {
        try
        {
            return await action;
        }
        catch (CloudSaveValidationException e)
        {
            _logger.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            _logger.LogError(e);
        }
        catch (CloudSaveException e)
        {
            _logger.LogError(e);
        }

        return default;
    }
}