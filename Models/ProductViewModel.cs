using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Models
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public ProductViewModel GetDefaultSet()
        {

            return new ProductViewModel()
            {
                Name = "Матан",
                Price = 199.99,
                ProductId = 0
            };
        }
    }
}
