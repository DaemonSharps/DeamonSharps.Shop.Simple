using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Models;
using DeamonSharps.Shop.Simple.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            var productsDB = new List<Product_DB>();
            if (categoryId == 0)
            {
                productsDB = await _productService.GetProductsFromDBAsync();
            }
            else
            {
                productsDB = await _productService.GetProductsFromDBByCategoryAsync(categoryId);

            }
            var products = productsDB
                .Select(p =>
                new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.About
                }).ToList();

            var model = new ShopPageViewModel
            {
                CategoryName = categoryName ?? "Все товары",
                Products = products
            };
            return View(model);
        }
    }
}
