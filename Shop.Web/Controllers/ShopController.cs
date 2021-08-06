using DeamonSharps.Shop.Simple.Api.Schemas;
using DeamonSharps.Shop.Simple.Entities;
using DeamonSharps.Shop.Simple.Extentions;
using DeamonSharps.Shop.Simple.Models;
using DeamonSharps.Shop.Simple.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;

        public ShopController(IProductService productService)
        {
            _productService = productService;
        }
        /// <summary>
        /// Страница с продуктами в категории или всеми товарами
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index(int categoryId, string categoryName)
        {
            var productsDB = await _productService.GetProductsFromDBByFilterAsync(1, categoryId);

            var products = productsDB
                .Select(p =>
                new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.About,
                    Count = HttpContext
                    .Session
                    .Get<Cart>("Cart")
                    ?.GetCartItem(p.Id)
                    ?.Count ?? 0
                }).ToList();

            var categories = (await _productService.GetCategoriesFromDBAsync())
                .Select(c =>
                new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();

            var model = new ShopPageViewModel
            {
                CategoryName = categoryName ?? "Все товары",
                Products = products,
                Categories = categories
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult GetProductCard([FromBody] Product product)
        {
            var model = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
            return PartialView("ProductCard", model);
        }
    }
}
