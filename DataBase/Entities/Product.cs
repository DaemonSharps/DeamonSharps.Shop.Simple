using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.DataBase.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Product_Name { get; set; }
        public decimal Product_Price { get; set; }
        public string Product_About { get; set; }
        public List<ProductCategory> ProductCategory { get; set; }
        public Product()
        {
            ProductCategory = new List<ProductCategory>();
        }
    }
}
