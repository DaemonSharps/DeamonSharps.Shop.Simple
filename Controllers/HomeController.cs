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
        /// Страница для вывода заказов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Orders(int page)
        {
            page = page == 0 ? 1 : page;
            var pageCount =  await _orderServiceController.GetPageCount();
            var orders = await _orderServiceController.GetOrdersByPage(page);

            var pageModel = new OrderPageViewModel
            {
                PageCount = pageCount,
                CurrentPage = page,
                Orders = orders.Select(order =>
                new OrderViewModel
                {
                    Id = order.Id,
                    CreationDate = order.Creation_Date,
                    Products = order.Order_Composition
                    .Where(oc => oc.Order_Id == order.Id)
                    .Select(oc => 
                    new CartProduct
                    {
                        Count = oc.ProductCount,
                        Product = new ProductViewModel
                        {
                            Name = oc.Product.Product_Name,
                            Price = oc.Product.Product_Price,
                            ProductId = oc.Product.Id
                        }
                    }).ToList(),
                PageCount = pageCount,
                CurrentPage = page
                }).ToList()
            };

            return View(pageModel);
        }
        /// <summary>
        /// Страница для просмотра и управления содержимым корзины
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
