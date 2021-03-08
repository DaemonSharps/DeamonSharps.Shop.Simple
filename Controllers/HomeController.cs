using DeamonSharps.Shop.Simple.Models;
using DeamonSharps.Shop.Simple.Services;
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
        private readonly ProductServiceController _productServiceController;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ProductServiceController productServiceController)
        {
            _logger = logger;
            _productServiceController = productServiceController;
            _products = productServiceController.GetProductsFromDB();
            
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
