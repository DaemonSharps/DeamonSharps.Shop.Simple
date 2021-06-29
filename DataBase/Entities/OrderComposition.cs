using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeamonSharps.Shop.Simple.DataBase.Entities
{
    [Table("order_сomposition")]
    public class OrderComposition
    {
        /// <summary>
        /// Номер заказа в БД
        /// </summary>
        [Key]
        public int Order_Id { get; set; }

        public Order Order { get; set; }

        /// <summary>
        /// Номер продукта в БД
        /// </summary>
        public int Product_Id { get; set; }

        public Product Product { get; set; }

        public int ProductCount { get; set; }
    }
}
