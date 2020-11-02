
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

        /// <summary>
        /// Belirilen key icerisinde deger yok ise olsturulur.
        /// AbsoluteExpiration : cache degeri 30 dakika boyunca RAM de tutulur.
        /// SlidingExpiration: gelen her istekte cache suresi 1 dakika artar.
        /// 
        /// Priority : Silinme onceligini belirtir.
        /// Low = 0,
        ///Normal = 1,
        ///High = 2,
        ///NeverRemove = 3/
        /// </summary>
        /// <param name="param"></param>
        [HttpGet("TryGetValueWithAbsoluteAndSlidingTime")]
        public string TryGetValueWithAbsoluteAndSlidingTime(string param)
        {
            

            if (_memoryCache.TryGetValue<string>(key, out string value))
            {
                return value;
            }
            else
            {
                var cacheExpirationOptions =
                   new MemoryCacheEntryOptions
                   {
                       AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                       Priority = CacheItemPriority.Normal,
                       SlidingExpiration = TimeSpan.FromSeconds(1)
                   };
                _memoryCache.Set(key, param, cacheExpirationOptions);

            }
            return param;
        }
        /// <summary>
        /// RegisterPostEvictionCallback : cache lenmiş datanın hangi sebeple memory den silindiginin bilgisini donduren metod
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetOrCreateWithRegisterPostEvictionCallback")]
        public string GetOrCreateWithRegisterPostEvictionCallback()
        {
            var result = "Empty";

            string date = _memoryCache.GetOrCreate<string>(key, entry =>
            {
                entry.RegisterPostEvictionCallback((key, value, reason, state) =>
                {
                   result =($"Key : {key}\nValue : {value}\nReason : {reason}\nState : {state}");
                });
                DateTime value = DateTime.Now;
                
                return value.ToString("dd.MM.yyyy");
            });

            return result;
        }
    }
}