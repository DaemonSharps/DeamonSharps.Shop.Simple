using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeamonSharps.Shop.Simple.Entities;

namespace DeamonSharps.Shop.Simple.Models
{
    [Serializable]
    public class CartViewModel
    {
        public List<CartProduct> Products { get; set; }
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
