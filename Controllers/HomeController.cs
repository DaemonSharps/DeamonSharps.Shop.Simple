using DeamonSharps.Shop.Simple.Entities;
using DeamonSharps.Shop.Simple.Extentions;
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

        public async Task<IActionResult> Index()
        {
            return View( await _categoryServiceController.GetCategoriesFromDBAsync());
        }


       
        [HttpGet]
        public async Task<IActionResult> Shop(int categoryId,string categoryName)
        {
            var products = new List<ProductViewModel>();
            if (categoryId == 0)
            {
                products = await _productServiceController.GetProductsFromDBAsync();
                ViewData["Category"] = "Все товары";
            }
            else {
                products = await _productServiceController.GetProductsFromDBByCategoryAsync(categoryId);
                ViewData["Category"] = categoryName;
            }
            
            return View(products);
        }
        public IActionResult Cart()
        {
            CartViewModel CartModel;
            var cart = HttpContext.Session.Get<Cart>("Cart");
            if (cart!=null)
            {
                 CartModel= new CartViewModel()
                {
                    Products = cart.Products.OrderBy(p=>p.Product.ProductId).ToList(),
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
