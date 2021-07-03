using System.Collections.Generic;

namespace DeamonSharps.Shop.Simple.Models
{
    /// <summary>
    /// Моделm страницы с продуктами
    /// </summary>
    public class ShopPageViewModel
    {
        public string Title { get; set; } = "Магазин";

        public string CategoryName { get; set; }

        public List<Product> Products { get; set; }
    }

    public struct Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
