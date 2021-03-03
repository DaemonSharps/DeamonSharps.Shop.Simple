using DeamonSharps.Shop.Simple.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Controllers
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
            return View();
        }

        public IActionResult Shop()
        {
            var products = new List<ProductViewModel>() {
                new ProductViewModel().GetDefaultSet(),
                new ProductViewModel().GetDefaultSet(),
                new ProductViewModel().GetDefaultSet(),
                new ProductViewModel().GetDefaultSet(),
                new ProductViewModel().GetDefaultSet(),
                new ProductViewModel().GetDefaultSet(),
                new ProductViewModel().GetDefaultSet(),
                new ProductViewModel().GetDefaultSet(),
                new ProductViewModel().GetDefaultSet(),
                new ProductViewModel().GetDefaultSet(),
                new ProductViewModel().GetDefaultSet(),
                new ProductViewModel().GetDefaultSet(),
                new ProductViewModel().GetDefaultSet()

            };



            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
