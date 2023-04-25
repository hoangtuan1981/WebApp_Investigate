using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using WebApp_Investigate.Intefaces;
using WebApp_Investigate.Models;

namespace WebApp_Investigate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICalculate _calculate;
        private readonly IEnumerable<ICalculate> _calculates;
        private readonly IReader _reader;
        private readonly IHelper _helper;
        private readonly IDistributedCache _cache;
        public HomeController(ILogger<HomeController> logger, ICalculate calculate, 
                IEnumerable<ICalculate> calculates,
                IReader reader, IHelper helper,
                IDistributedCache cache)
        {
            _logger = logger;
            _calculate = calculate;
            _calculates = calculates;
            _reader = reader;
            _helper = helper;
            _cache = cache;
        }

        public IActionResult Index()
        {
            var cal = _calculate.Calculate();

            _logger.LogInformation($"Calculate: {cal}");
            _logger.LogInformation($"IReader == IHelper:  {_reader == _helper}");

            //await GetFromCache();

            return View();
        }

        private async Task GetFromCache()
        {
            var cachedTimeUTCKey = "Cached Time Expired";
            var encodedCachedTimeUTC = await _cache.GetAsync("cachedTimeUTC");
            
            if (encodedCachedTimeUTC != null)
            {
                //From Cache
                cachedTimeUTCKey = Encoding.UTF8.GetString(encodedCachedTimeUTC);
            }
            else
            {
                //From DB:
            }
        }

        public async Task<IActionResult> OnPostResetCachedTime()
        {
            var currentTimeUTC = DateTime.UtcNow.ToString();
            byte[] encodedCurrentTimeUTC = Encoding.UTF8.GetBytes(currentTimeUTC);
            var options = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(20));
            await _cache.SetAsync("cachedTimeUTC", encodedCurrentTimeUTC, options);

            return View();//RedirectToPage();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
