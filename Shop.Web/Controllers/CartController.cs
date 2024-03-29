﻿using DeamonSharps.Shop.Simple.Entities;
using DeamonSharps.Shop.Simple.Extentions;
using DeamonSharps.Shop.Simple.Models;
using DeamonSharps.Shop.Simple.Services.Interfaces;
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
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        /// <summary>
        /// Контроллер для управления корзиной с заказом
        /// </summary>
        /// <param name="productService">Сервис для подгрузки продуктов из БД</param>
        /// <param name="orderService">Сервис для работы с заказами в БД</param>
        public CartController(IProductService productService,
            IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        /// <summary>
        /// Страница для просмотра и управления содержимым корзины
        /// </summary>
        public async Task<IActionResult> Index()
        {
            CartPageViewModel CartModel;
            var cart = GetCart();
            if (cart != null)
            {
                var products = await _productService.GetProductsFromDBByIdsAsync(cart.Products.Select(p => p.ProductId));
                var totalPrice = cart.Products.Sum(p => p.Count * products.Single(pr => pr.Id == p.ProductId).Price);

                CartModel = new CartPageViewModel
                {
                    TotalPrice = totalPrice,
                    Products = cart.Products
                    .Select(p =>
                    new CartItemViewModel
                    {
                        Count = p.Count,
                        Product = products
                        .Where(pr => pr.Id == p.ProductId)
                        .Select(pr => new ProductViewModel
                        {
                            Id = pr.Id,
                            Name = pr.Name,
                            Price = pr.Price,
                            Description = pr.About
                        }).Single()
                    }).OrderBy(p => p.Product.Id).ToList()
                };
                ModelState.Clear();
            }
            else
            {
                CartModel = CartPageViewModel.GetDefaultSet();
            }

            return View(CartModel);
        }

        /// <summary>
        /// Обновляет количество едениц продукта в корзине
        /// </summary>
        /// <param name="prodId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IActionResult UpdateCart(int prodId, int count)
        {
            GetCart().ChangeProductCount(prodId, count, HttpContext);

            return Ok();
        }

        /// <summary>
        /// Удалить продукт из корзины
        /// </summary>
        /// <param name="id">Номер продукта в БД</param>
        public IActionResult Delete(int id)
        {
            GetCart().Delete(id, HttpContext);
            return Ok();
        }
        /// <summary>
        /// Очистить корзину
        /// </summary>
        /// <param name="returnUrl">Адресс страницы, на которую нужно вернуться</param>
        public IActionResult Clear(string returnUrl)
        {
            GetCart().Clean(HttpContext);
            return LocalRedirect("~" + returnUrl);
        }

        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <param name="returnUrl">Адресс страницы, на которую нужно вернуться</param>
        /// <param name="products">Список продуктов</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrder([Required] IEnumerable<CartItem> products, [Required] string returnUrl)
        {
            await _orderService.CreateOrderInDBAsync(products);

            GetCart().Clean(HttpContext);

            return LocalRedirect("~" + returnUrl);
        }

        public IActionResult GetCartProductCount(int productId)
        {
            var count = GetCart().Products.Where(p => p.ProductId == productId).FirstOrDefault()?.Count ?? 0;

            return Ok(count);
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
