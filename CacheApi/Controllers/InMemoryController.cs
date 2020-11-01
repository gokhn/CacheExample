
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;

namespace CacheApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InMemoryController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<InMemoryController> _logger;

        private string key = "GokhnGungor";

        public InMemoryController(ILogger<InMemoryController> logger,IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        [HttpGet("setcache")]
        public string SetCache(string value)
        {            
            _memoryCache.Set(key, value);

            return value + " Added";
        }

        [HttpGet("getcache")]
        public string GetCache()
        {
            var result = _memoryCache.Get(key);

            return  result != null ?  result.ToString() : "";
        }

        [HttpGet("removecache")]
        public void RemoveCache()
        {
            _memoryCache.Remove(key);
        }

        /// <summary>
        /// key: Cache yer alan key bilgisi
        /// value: Belirtilen key bilgisinin degeri value icerisine yazar
        /// </summary>
        /// <returns></returns>
        [HttpGet("trygetvalue")]
        public string TryGetValue()
        {
           if( _memoryCache.TryGetValue<string>(key,out string value))
            {
                return value;
            }

            return "Cache Not Found !!";
        }

        /// <summary>
        /// Belirtilen key degerinda data var mı diye kontrol edilir. Eğer yok ise oluşturulur.
        /// </summary>
        /// <param name="value">Cache'e yazilacak deger</param>
        /// <returns></returns>
        [HttpGet("getOrCreate")]
        public string GetOrCreate(string value)
        {
            string result = _memoryCache.GetOrCreate<string>(key, entry =>
            {
                entry.SetValue(value);                
                return entry.Value.ToString();
            });

            return result;
        }

        [HttpGet("getOrCreate")]
        public string GetOrCreateWithAbsoluteAndSlidingTime(string value)
        {
            //string result = _memoryCache.GetOrCreate<string>(key, entry =>
            //{
            //    entry.SetValue(value);
            //    entry.AbsoluteExpiration = DateTime.Now.AddSeconds(30);//Cache'de ki datanın ömrü 10 saniye olarak belirlenmiştir.
            //    entry.SlidingExpiration = TimeSpan.FromSeconds(5);//Cache'de ki datanın ömrü 2 saniye olarak belirlenmiştir.
            //                                                      //2 saniye içerisinde bir istek yapılırsa kalış süresi 2 saniye daha uzayacaktır.
            //                                                      //Absolute değeri belirtildiğinden dolayı bu süreç totalde 2 saniye boyunca sürecektir.

            //});

            //var cacheExpirationOptions = new MemoryCacheEntryOptions
            //   {
            //       AbsoluteExpiration = DateTime.Now.AddMinutes(30),
            //       SlidingExpiration = TimeSpan.FromSeconds(0.5),
            //       Priority = CacheItemPriority.Normal
            //   };

            //return result;
            return "";
        }
    }
}