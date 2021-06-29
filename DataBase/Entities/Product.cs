using System.Collections.Generic;

namespace DeamonSharps.Shop.Simple.DataBase.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Product_Name { get; set; }
        public decimal Product_Price { get; set; }
        public string Product_About { get; set; }
        public List<ProductCategory> ProductCategory { get; set; } = new List<ProductCategory>();

        public List<OrderComposition> Order_Composition { get; set; } = new List<OrderComposition>();

    }
}
