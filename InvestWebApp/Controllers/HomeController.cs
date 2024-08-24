using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public HomeController(ILogger<HomeController> logger, ICalculate calculate, 
                IEnumerable<ICalculate> calculates,
                IReader reader, IHelper helper)
        {
            _logger = logger;
            _calculate = calculate;
            _calculates = calculates;
            _reader = reader;
            _helper = helper;
        }

        public IActionResult Index()
        {
            var cal = _calculate.Calculate();

            _logger.LogInformation($"Calculate: {cal}");
            _logger.LogInformation($"IReader == IHelper:  {_reader == _helper}");
            return View();
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
