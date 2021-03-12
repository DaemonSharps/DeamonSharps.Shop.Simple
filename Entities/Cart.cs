using DeamonSharps.Shop.Simple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using DeamonSharps.Shop.Simple.Extentions;
using Microsoft.AspNetCore.Http;

namespace DeamonSharps.Shop.Simple.Entities
{
    /// <summary>
    /// Класс корзины
    /// </summary>
    [Serializable]
    public class Cart
    {
        private readonly List<CartProduct> productsList;
        
        /// <summary>
        /// Класс корзины
        /// </summary>
        public Cart()
        {
            productsList = new List<CartProduct>();
        }
        /// <summary>
        /// Получить список продуков в корзине
        /// </summary>
        public List<CartProduct> Products {
            get {
                return productsList;
            }
        }
        /// <summary>
        /// Добавить продукт в корзину
        /// </summary>
        /// <param name="product">Добавляемый продукт</param>
        public void Add(ProductViewModel product,HttpContext context)
        {
            CartProduct existedProduct = productsList
                ?.Where(p => p.Product.Name == product.Name)
                .FirstOrDefault();
            if (existedProduct == null)
            {
                productsList.Add(new CartProduct
                {
                    Product = product,
                    Count = +1
                });
                context.Session.Set("Cart",this);
                
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
        /// <param name="product">Удаляемый продукт</param>
        public void Delete(string Name, HttpContext context)
        {
           var product= productsList.Where(p => p.Product.Name == Name).FirstOrDefault();
            productsList.Remove(product);
            product.Count--;
            if (product.Count>0)
            {
                productsList.Add(product);
            }
           
            
            context.Session.Set("Cart", this);
        }
        /// <summary>
        /// Свойство для получения общей стоимости товаров в корзине
        /// </summary>
        public decimal TotalPrice{
            get
            {
              return  productsList.Sum(p=>p.Count*p.Product.Price);
            }
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
}
