using DeamonSharps.Shop.Simple.Api.Services;
using DeamonSharps.Shop.Simple.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductServiceController _productServiceController;
        private readonly CategoryServiceController _categoryServiceController;
        private readonly OrderServiceController _orderServiceController;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            ProductServiceController productServiceController,
            CategoryServiceController categoryServiceController,
            OrderServiceController orderServiceController)
        {
            _logger = logger;
            _productServiceController = productServiceController;
            _categoryServiceController = categoryServiceController;
            _orderServiceController = orderServiceController;
        }
        /// <summary>
        /// Начальная страница с категориями продуктов
        /// </summary>
        public async Task<IActionResult> Index()
        {
            return View(await _categoryServiceController.GetCategoriesFromDBAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
