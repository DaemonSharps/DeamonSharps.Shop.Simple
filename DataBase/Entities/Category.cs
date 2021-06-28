using System.Collections.Generic;

namespace DeamonSharps.Shop.Simple.DataBase.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Category_Name { get; set; }

        public List<ProductCategory> ProductCategory { get; set; }
        public Category()
        {
            ProductCategory = new List<ProductCategory>();
        }
    }
}
