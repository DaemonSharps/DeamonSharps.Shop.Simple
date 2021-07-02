using DeamonSharps.Shop.Simple.Models;
using DeamonSharps.Shop.Simple.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Controllers
{
    public class ShopController : Controller
    {
        private readonly ProductServiceController _productServiceController;

        public ShopController(ProductServiceController productServiceController)
        {
            _productServiceController = productServiceController;
        }
        /// <summary>
        /// Страница с продуктами в категории или всеми товарами
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index(int categoryId, string categoryName)
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
    }
}
