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
        private List<CartItem> productsList;

        /// <summary>
        /// Класс корзины
        /// </summary>
        public Cart(params CartItem[] items)
        {
            productsList = new List<CartItem>();
            productsList.AddRange(items);
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

        public void ChangeProductCount(int prodId, int count, HttpContext context)
        {
            CartItem existedProduct = productsList?.FirstOrDefault(p => p.ProductId == prodId);
            if (existedProduct == null)
            {
                Add(prodId, context, count);
            }
            else if (existedProduct != null && count == 0)
            {
                Delete(prodId, context);
            }
            else
            {
                existedProduct.Count = count;
                context.Session.Set("Cart", this);
            }
        }

        /// <summary>
        /// Добавить продукт в корзину
        /// </summary>
        /// <param name="prodId">Номер добавляемого продукта</param>
        /// <param name="count">Количество едениц продукта</param>
        /// <param name="context"></param>
        private void Add(int prodId, HttpContext context, int count = 0)
        {
            productsList.Add(new CartItem
            {
                ProductId = prodId,
                Count = count == 0 ? 1 : count
            });
            context.Session.Set("Cart", this);
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
