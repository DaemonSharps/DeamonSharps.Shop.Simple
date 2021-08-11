using System.Collections.Generic;

namespace DeamonSharps.Shop.Simple.Models
{
    /// <summary>
    /// Модель страницы с продуктами
    /// </summary>
    public class ShopPageViewModel
    {
        public string Title { get; set; } = "Магазин";

        public int CategoryId { get; set; }

        public List<CategoryViewModel> Categories { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
