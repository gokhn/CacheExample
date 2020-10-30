using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace RedisExample.Controllers
{
    public class PersonelController : Controller
    {
        IMemoryCache _memoryCache;
        public PersonelController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public IActionResult Index()
        {

            _memoryCache.Set("PersonName", "GokhanG");

            string name = _memoryCache.Get<string>("PersonName");

            return View();
        }

        public IActionResult SetCache()
        {
            _memoryCache.Set("PersonName", "GokhanG");
            return View();
        }

        public IActionResult GetCache()
        {
            string name = _memoryCache.Get<string>("PersonName");
            return View();
        }
    }
}