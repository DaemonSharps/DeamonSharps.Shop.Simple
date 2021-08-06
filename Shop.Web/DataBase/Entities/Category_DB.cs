using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeamonSharps.Shop.Simple.DataBase.Entities
{
    /// <summary>
    /// Категория продукта
    /// </summary>
    [Table("categories")]
    public class Category_DB
    {
        /// <summary>
        /// Номер категории
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название категории
        /// </summary>
        [Required]
        [Column("Category_Name")]
        [MaxLength(100)]
        public string Name { get; set; }

        public List<ProductCategory_DB> ProductCategory { get; set; } = new List<ProductCategory_DB>();
    }
}
