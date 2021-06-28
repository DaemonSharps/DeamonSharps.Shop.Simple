using System;

namespace DeamonSharps.Shop.Simple.Models
{
    [Serializable]
    public class ProductViewModel
    {
        /// <summary>
        /// Название продукт
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена продукта
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Номер продукта
        /// </summary>
        public int ProductId { get; set; }

        public ProductViewModel GetDefaultSet()
        {

            return new ProductViewModel()
            {
                Name = "Матан",
                Price = 199.99M,
                ProductId = 0
            };
        }
    }
}
