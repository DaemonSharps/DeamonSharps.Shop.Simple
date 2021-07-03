using DeamonSharps.Shop.Simple.DataBase.Context;
using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Extentions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeamonSharps.Shop.Simple.Entities
{
    /// <summary>
    /// Класс корзины
    /// </summary>
    [Serializable]
    public class Cart
    {
        private readonly List<CartItem> productsList;

        /// <summary>
        /// Класс корзины
        /// </summary>
        public Cart()
        {
            productsList = new List<CartItem>();
        }

        /// <summary>
        /// Получить список продуков в корзине
        /// </summary>
        public List<CartItem> Products
        {
            get
            {
                return productsList;
            }
        }

        /// <summary>
        /// Добавить продукт в корзину
        /// </summary>
        /// <param name="product">Добавляемый продукт</param>
        /// <param name="context"></param>
        public void Add(Product_DB product, HttpContext context)
        {
            CartItem existedProduct = productsList
                ?.Where(p => p.ProductId == product.Id)
                .FirstOrDefault();
            if (existedProduct == null)
            {
                productsList.Add(new CartItem
                {
                    ProductId = product.Id,
                    Count = +1
                });
                context.Session.Set("Cart", this);

            }
            else
            {
                existedProduct.Count++;
                context.Session.Set("Cart", this);
            }
        }

        /// <summary>
        /// Удалить продукт из корзины
        /// </summary>
        /// <param name="id">Номер продукта в БД</param>
        /// <param name="context"></param>
        public void Delete(int id, HttpContext context)
        {
            var product = productsList.Where(p => p.ProductId == id).FirstOrDefault();
            productsList.Remove(product);
            if (product != null)
            {
                product.Count--;


                if (product.Count > 0)
                {
                    productsList.Add(product);
                }
            }

            context.Session.Set("Cart", this);
        }

        public CartItem GetCartItem(int id)
        {
            return productsList.FirstOrDefault(p => p.ProductId == id);
        }

        /// <summary>
        /// Очистить корзину
        /// </summary>
        public void Clean(HttpContext context)
        {
            productsList.Clear();
            context.Session.Set("Cart", this);
        }

    }

    [Serializable]
    public class CartItem
    {
        /// <summary>
        /// Продукт в корзине
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Количество единиц продуктв
        /// </summary>
        public int Count { get; set; }
    }
}
