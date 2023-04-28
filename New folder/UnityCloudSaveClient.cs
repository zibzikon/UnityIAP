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
    public static UnityCloudSaveClient Instance { get; } = new ();

    private readonly ICloudSaveDataClient _client =  CloudSaveService.Instance.Data;
    private readonly ILogger _logger;


    public async Task SaveAsync(string key, object value)
    {
        var data = new Dictionary<string, object> { { key, value } };
        await Call(_client.ForceSaveAsync(data));
    }

    public async Task SaveAsync(params (string key, object value)[] values)
    {
        var data = values.ToDictionary(item => item.key, item => item.value);
        await Call(_client.ForceSaveAsync(data));
    }

    public async Task<T> LoadAsync<T>(string key)
    {
        var query = await Call(_client.LoadAsync(new HashSet<string> { key }));
        
        return query.TryGetValue(key, out var value) ? Deserialize<T>(value) : default;
    }

    public async Task<IEnumerable<T>> LoadAsync<T>(params string[] keys)
    {
        var query = await Call(_client.LoadAsync(keys.ToHashSet()));

        return keys.Select(k =>
        {
            if (query.TryGetValue(k, out var value))
            {
                return value != null ? Deserialize<T>(value) : default;
            }

            return default;
        });
    }

    public async Task DeleteAsync(string key) => await Call(_client.ForceDeleteAsync(key));

    public async Task DeleteAllAsync()
    {
        var keys = await Call(_client.RetrieveAllKeysAsync());
        
        var tasks = keys.Select(DeleteAsync);
        
        await Call(Task.WhenAll(tasks));
    }

    private T Deserialize<T>(string token)
    {
        if (typeof(T) == typeof(string)) return (T)(object)token;
        return JsonConvert.DeserializeObject<T>(token);;
    }

    private async Task Call(Task action)
    {
        try
        {
            await action;
            
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
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
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }

        return default;
    }
}