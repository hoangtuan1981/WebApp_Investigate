using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using WebApp_Investigate.Intefaces;
using WebApp_Investigate.Models;

/* Links to expose redis on docker
 https://markheath.net/post/exploring-redis-with-docker
 */
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

        public async Task<IActionResult> Index()
        {
            var cal = _calculate.Calculate();

            _logger.LogInformation($"Calculate: {cal}");
            _logger.LogInformation($"IReader == IHelper:  {_reader == _helper}");
            //var encodedCachedTimeUTC =  _cache.GetString("cachedTimeUTC");
            await GetSetRedisCache();

            return View();
        }

        private async Task GetSetRedisCache()
        {
            //cache a string
            var cachedTimeUTCKey = "Cached Time Expired";
            _cache.SetString("key1", "test");
            var encodedCachedTimeUTC = _cache.GetString("key1");
            var emp = new Employee();
            emp.EmployeeId = 1;
            emp.FirstName = "Tuấn";
            emp.LastName = "Hoàng";
            var emp1 = new Employee();
            emp1.EmployeeId = 1;
            emp1.FirstName = "Bảo";
            emp1.LastName = "Hoàng";
            string strJson = JsonSerializer.Serialize<Employee>(emp);
            //cache object as string
            _cache.SetString("key2", strJson);

            var strEmp = _cache.GetString("key2");
            var emp2 = JsonSerializer.Deserialize<Employee>(strEmp);
            var emps1 = new List<Employee>();
            emps1.Add(emp);
            emps1.Add(emp1);

            strJson = JsonSerializer.Serialize<List<Employee>>(emps1);
            //cache list object as string
            _cache.SetString("key3", strJson);

            var strEmps = _cache.GetString("key3");
            var emps2 = JsonSerializer.Deserialize<List<Employee>>(strEmps);

            if (encodedCachedTimeUTC != null)
            {
                //From Cache
                //cachedTimeUTCKey = Encoding.UTF8.GetString(encodedCachedTimeUTC);
            }
            else
            {
                //From DB:
            }

            //return cachedTimeUTCKey;
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
