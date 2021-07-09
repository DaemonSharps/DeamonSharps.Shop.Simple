using DeamonSharps.Shop.Simple.Models;
using DeamonSharps.Shop.Simple.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        /// <summary>
        /// Начальная страница с категориями продуктов
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var categories = await _productService.GetCategoriesFromDBAsync();
            var model = new CategoryPageViewModel
            {
                Categories = categories
                .Select(c =>
                new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList()
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
