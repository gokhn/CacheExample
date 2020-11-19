#  InMemory Caching ve Distributed Caching Kullanımı

Web Api ve Mvc Ui  üzerinde  InMemory ve Distributed kullanım örnekleri



## InMemory 

```c#
_memoryCache.Set(key, value);
_memoryCache.Get(key);
_memoryCache.Remove(key);
_memoryCache.TryGetValue<string>(key,out string value); _memoryCache.GetOrCreate<string>(key, entry =>
            {
                entry.SetValue(value);                
                return entry.Value.ToString();
                
            });


```

## Distributed 

```c#
Set(string key, byte[] value, DistributedCacheEntryOptions options);
Get(string key);
Remove(key);


```
## Referanslar
* https://docs.microsoft.com/tr-tr/aspnet/core/performance/caching/memory?view=aspnetcore-3.1
* https://www.gencayyildiz.com/blog/redis-yazi-dizisi/
* http://www.canertosuner.com/post/asp-net-core-in-memory-cache
* http://devnot.com/2020/stackexchange-redis-ile-distributed-caching/
