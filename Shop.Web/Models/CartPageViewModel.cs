using System.Collections.Generic;

namespace DeamonSharps.Shop.Simple.Models
{
    public class CartPageViewModel
    {
        public string Title { get; set; } = "Корзина";

        public List<CartItemViewModel> Products { get; set; }

        public decimal TotalPrice { get; set; }

        public static CartPageViewModel GetDefaultSet()
        {
            return new CartPageViewModel
            {
                Products = null,
                TotalPrice = 0
            };
        }
    }

    public struct CartItemViewModel
    {
        public ProductViewModel Product { get; set; }

        public int Count { get; set; }
    }
}
