using System.ComponentModel.DataAnnotations;

namespace DeamonSharps.Shop.Simple.DataBase.Entities
{
    public class ProductCategory
    {
        [Key]
        public int Product_Id { get; set; }
        public Product Product { get; set; }
        public int Category_Id { get; set; }
        public Category Category { get; set; }
    }
}
