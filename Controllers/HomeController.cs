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
        private List<ProductViewModel> _products;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _products = new List<ProductViewModel>();
            for (int i = 0; i < 9; i++)
            {
                _products.Add(new ProductViewModel().GetDefaultSet());
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Shop()
        {

            return View(_products);
        }
        public IActionResult Cart()
        {
            return View(_products);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
