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
        private readonly ICookieProvider _cookieProvider;

        public ShopController(IProductService productService, ICookieProvider cookieProvider)
        {
            _productService = productService;
            _cookieProvider = cookieProvider;
        }
        /// <summary>
        /// Страница с продуктами в категории или всеми товарами
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categoryId = int.Parse(_cookieProvider.GetCookieValue("categoryId") ?? "0");
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
                    Name = c.Name,
                    State = c.Id == categoryId ? "active" : null
                }).ToList();

            var model = new ShopPageViewModel
            {
                CategoryId = categoryId,
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
