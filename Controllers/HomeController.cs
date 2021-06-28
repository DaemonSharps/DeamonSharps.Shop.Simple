using DeamonSharps.Shop.Simple.Entities;
using DeamonSharps.Shop.Simple.Extentions;
using DeamonSharps.Shop.Simple.Models;
using DeamonSharps.Shop.Simple.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductServiceController _productServiceController;
        private readonly CategoryServiceController _categoryServiceController;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            ProductServiceController productServiceController,
            CategoryServiceController categoryServiceController)
        {
            _logger = logger;
            _productServiceController = productServiceController;

            _categoryServiceController = categoryServiceController;

        }
        /// <summary>
        /// Начальная страница с категориями продуктов
        /// </summary>
        public async Task<IActionResult> Index()
        {
            return View(await _categoryServiceController.GetCategoriesFromDBAsync());
        }

        /// <summary>
        /// Страница с продуктами в категории или всеми товарами
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Shop(int categoryId, string categoryName)
        {
            var products = new List<ProductViewModel>();
            if (categoryId == 0)
            {
                products = await _productServiceController.GetProductsFromDBAsync();
                ViewData["Category"] = "Все товары";
            }
            else
            {
                products = await _productServiceController.GetProductsFromDBByCategoryAsync(categoryId);
                ViewData["Category"] = categoryName;
            }

            return View(products);
        }
        /// <summary>
        /// Страница для просмотра и управления содержимым страницы
        /// </summary>
        public IActionResult Cart()
        {
            CartViewModel CartModel;
            var cart = HttpContext.Session.Get<Cart>("Cart");
            if (cart != null)
            {
                CartModel = new CartViewModel()
                {
                    Products = cart.Products.OrderBy(p => p.Product.ProductId).ToList(),
                    TotalPrice = cart.TotalPrice
                };
            }
            else
            {
                CartModel = null;
            }

            return View(CartModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
