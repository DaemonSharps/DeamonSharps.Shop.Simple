using DeamonSharps.Shop.Simple.Models;
using DeamonSharps.Shop.Simple.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Controllers
{
    public class HomeController : Controller
    {
        private readonly CategoryServiceController _categoryServiceController;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            CategoryServiceController categoryServiceController)
        {
            _logger = logger;
            _categoryServiceController = categoryServiceController;
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
