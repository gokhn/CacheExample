using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CacheUI.Models;
using System.Text;

namespace CacheUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // return View();
            var name = "Gokhan Gungor";

            HttpContext.Session.Set("session_Example", Encoding.UTF8.GetBytes(name));
            return Ok(name);
        }        
        public IActionResult Privacy()
        {
            // return View();
            var response = "Empty";
            if (HttpContext.Session.TryGetValue("session_Example", out byte[] value))
                response = Encoding.UTF8.GetString(value);


            return Ok(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
