using System.Collections.Generic;

namespace DeamonSharps.Shop.Simple.DataBase.Entities
{
    public class Category
    {
        /// <summary>
        /// Номер категории
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название категории
        /// </summary>
        public string Category_Name { get; set; }

        public List<ProductCategory> ProductCategory { get; set; } = new List<ProductCategory>();
    }
}
