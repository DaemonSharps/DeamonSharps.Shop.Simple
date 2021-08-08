using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeamonSharps.Shop.Simple.DataBase.Entities
{
    /// <summary>
    /// Продукт
    /// </summary>
    [Table("products")]
    public class Product_DB
    {
        /// <summary>
        /// Номер продукта в БД
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название продукта
        /// </summary>
        [Required]
        [MaxLength(200)]
        [Column("Product_Name")]
        public string Name { get; set; }

        /// <summary>
        /// Цена продукта
        /// </summary>
        [Required]
        [Column("Product_Price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Описание продукта
        /// </summary>
        [MaxLength(400)]
        [Column("Product_About")]
        public string About { get; set; }

        /// <summary>
        /// Связ many-to-many с категориями
        /// </summary>
        public List<ProductCategory_DB> ProductCategory { get; set; } = new List<ProductCategory_DB>();

        /// <summary>
        /// Связ many-to-many с продуктами в заказе
        /// </summary>
        public List<OrderComposition_DB> Order_Composition { get; set; } = new List<OrderComposition_DB>();

        public static Product_DB GetDefaultValue(int index = 1)
        {
            return new Product_DB
            {
                Id = index,
                Name = $"name{index}",
                Price = index,
                About = $"about{index}"
            };
        }
    }
}
