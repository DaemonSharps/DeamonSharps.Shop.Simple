using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeamonSharps.Shop.Simple.DataBase.Entities
{
    [Table("Order_Status")]
    public class OrderStatus
    {
        /// <summary>
        /// Номер статуса
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название статуса
        /// </summary>
        public string Name { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
