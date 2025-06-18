namespace Utils;

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

public class MemoryCacheProvider<Key, Value> : ICacheProvider<Key, Value>
{
    private readonly IMemoryCache _cache;   

    public MemoryCacheProvider()
    {
        _cache = new MemoryCache(new MemoryCacheOptions());         
    }

    public bool TryGetValue(Key key, out Value val)
    {
        return _cache.TryGetValue(key, out val);
    }

    public void Set(Key key, Value value, TimeSpan duration)
    {
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = duration
        };

        _cache.Set(key, value, options);
    }

    public void Set(Dictionary<Key, Value> items, TimeSpan duration)
    {
        foreach (var kvp in items)
        {
            Set(kvp.Key, kvp.Value, duration);
        }
    }
}
