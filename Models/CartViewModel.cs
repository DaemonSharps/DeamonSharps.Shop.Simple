using System;
using System.Collections.Generic;

namespace DeamonSharps.Shop.Simple.Models
{
    [Serializable]
    public class CartViewModel
    {
        /// <summary>
        /// Список продуктов в корзине
        /// </summary>
        public List<CartProduct> Products { get; set; }
        /// <summary>
        /// Суммарная стоимость корзины
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
    /// <summary>
    /// Класс, определяющий элемент корзины
    /// </summary>
    [Serializable]
    public class CartProduct
    {
        /// <summary>
        /// Продукт в корзине
        /// </summary>
        public ProductViewModel Product { get; set; }
        /// <summary>
        /// Количество единиц продуктв
        /// </summary>
        public int Count { get; set; }
    }
}
