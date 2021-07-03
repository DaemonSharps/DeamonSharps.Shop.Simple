using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeamonSharps.Shop.Simple.DataBase.Entities
{
    /// <summary>
    /// many-yo-many заказа и продуктов
    /// </summary>
    [Table("order_сomposition")]
    public class OrderComposition_DB
    {
        /// <summary>
        /// Номер заказа в БД
        /// </summary>
        [Key]
        public int Order_Id { get; set; }

        public Order_DB Order { get; set; }

        /// <summary>
        /// Номер продукта в БД
        /// </summary>
        public int Product_Id { get; set; }

        public Product_DB Product { get; set; }

        public int ProductCount { get; set; }
    }
}
