using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeamonSharps.Shop.Simple.DataBase.Entities
{
    /// <summary>
    /// Many-to-many продукты и категории
    /// </summary>
    [Table("productcategory")]
    public class ProductCategory_DB
    {
        /// <summary>
        /// Номер продукта в БД
        /// </summary>
        [Key]
        public int Product_Id { get; set; }

        /// <summary>
        /// Поле дле реализации саязи many-to-many
        /// </summary>
        public Product_DB Product { get; set; }

        /// <summary>
        /// Номер категории в БД
        /// </summary>
        public int Category_Id { get; set; }

        /// <summary>
        /// Поле дле реализации саязи many-to-many
        /// </summary>
        public Category_DB Category { get; set; }
    }
}
