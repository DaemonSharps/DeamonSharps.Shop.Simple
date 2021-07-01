using DeamonSharps.Shop.Simple.Entities;
using DeamonSharps.Shop.Simple.Extentions;
using DeamonSharps.Shop.Simple.Models;
using DeamonSharps.Shop.Simple.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductServiceController _productServiceController;
        private readonly OrderServiceController _orderServiceController;
        /// <summary>
        /// Контроллер для управления корзиной с заказом
        /// </summary>
        /// <param name="productServiceController">Сервис для подгрузки продуктов из БД</param>
        /// <param name="orderServiceController">Сервис для работы с заказами в БД</param>
        public CartController(ProductServiceController productServiceController, OrderServiceController orderServiceController)
        {
            _productServiceController = productServiceController;
            _orderServiceController = orderServiceController;
        }

        /// <summary>
        /// Страница для просмотра и управления содержимым корзины
        /// </summary>
        public IActionResult Index()
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

        /// <summary>
        /// Добавить продукт в корзину
        /// </summary>
        /// <param name="Name">Имя продукта</param>
        /// <param name="returnUrl">Адресс страницы, на которую нужно вернуться</param>
        /// <returns></returns>
        public async Task<IActionResult> Add(string Name, string returnUrl)
        {
            var products = await _productServiceController.GetProductsFromDBAsync();
            var product = products.Where(p => p.Name == Name)
                .FirstOrDefault();
            if (product != null)
            {

                GetCart().Add(product, HttpContext);
            }
            return LocalRedirect("~" + returnUrl);
        }
        /// <summary>
        /// Удалить продукт из корзины
        /// </summary>
        public IActionResult Delete(string Name, string returnUrl)
        {
            GetCart().Delete(Name, HttpContext);
            return LocalRedirect("~" + returnUrl);
        }
        /// <summary>
        /// Очистить корзину
        /// </summary>
        public IActionResult Clear(string returnUrl)
        {
            GetCart().Clean(HttpContext);
            return LocalRedirect("~" + returnUrl);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([Required] string returnUrl, [Required] IEnumerable<CartProduct> products)
        {
            await _orderServiceController.CreateOrderInDB(products);

            GetCart().Clean(HttpContext);

            return LocalRedirect("~" + returnUrl);
        }
        /// <summary>
        /// Получает экземпляр корзины из сессии либо создает пустую
        /// </summary>
        /// <returns></returns>
        private Cart GetCart()
        {
            Cart existedCart = HttpContext.Session.Get<Cart>("Cart");
            if (existedCart == null)
            {
                existedCart = new Cart();
                HttpContext.Session.Set("Cart", existedCart);
            }
            return existedCart;
        }

    }

}
