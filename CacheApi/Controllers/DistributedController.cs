using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace CacheApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributedController : ControllerBase
    {
        IDistributedCache _distributedCache;
        public DistributedController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet("CacheSetString")]
        public IActionResult CacheSetString()
        {
            var data = DateTime.Now.ToString();

            _distributedCache.SetString("date", data,
          new DistributedCacheEntryOptions {
            AbsoluteExpiration = DateTime.Now.AddSeconds(120),
            SlidingExpiration = TimeSpan.FromSeconds(60)
            
            });
                        
            return Ok(data);
        }

        [HttpGet("CacheGetString")]
        public IActionResult CacheGetString()
        {
            string value = _distributedCache.GetString("date");
            return Ok(value);
        }

        [HttpGet("CacheRemove")]
        public IActionResult CacheRemove()
        {
            _distributedCache.Remove("date");
            return Ok();
        }
    }
}