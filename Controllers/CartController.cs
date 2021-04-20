using DeamonSharps.Shop.Simple.Entities;
using DeamonSharps.Shop.Simple.Extentions;
using DeamonSharps.Shop.Simple.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductServiceController _productServiceController;
        /// <summary>
        /// Контроллер для управления корзиной с заказом
        /// </summary>
        /// <param name="productServiceController">Сервис для подгрузки продуктов из БД</param>
        public CartController(ProductServiceController productServiceController)
        {
            _productServiceController = productServiceController;
        }
        /// <summary>
        /// Добавить продукт в корзину
        /// </summary>
        /// <param name="Name">Имя продукта</param>
        /// <param name="returnUrl">Адресс страницы, на которую нужно вернуться</param>
        /// <returns></returns>
        public async Task<IActionResult> Add(string Name,string returnUrl)
        {
            var products = await _productServiceController.GetProductsFromDBAsync();
            var product=   products.Where(p => p.Name == Name)
                .FirstOrDefault();
            if (product!=null)
            {

               GetCart().Add(product,HttpContext);
            }
            return LocalRedirect("~"+returnUrl);
        }
        /// <summary>
        /// Удалить продукт из корзины
        /// </summary>
        public IActionResult Delete(string Name,string returnUrl)
        {
            GetCart().Delete(Name,HttpContext);
            return LocalRedirect("~"+returnUrl);
        }
        /// <summary>
        /// Очистить корзину
        /// </summary>
        public IActionResult Clear(string returnUrl)
        {
            GetCart().Clean(HttpContext);
            return LocalRedirect("~"+returnUrl);
        }
        /// <summary>
        /// Получает экземпляр корзины из сессии либо создает пустую
        /// </summary>
        /// <returns></returns>
        private Cart GetCart()
        {
           Cart existedCart= HttpContext.Session.Get<Cart>("Cart");
            if (existedCart==null)
            {
                existedCart = new Cart();
                HttpContext.Session.Set("Cart",existedCart);
            } 
            return existedCart;
        }
        
    }
    
}
