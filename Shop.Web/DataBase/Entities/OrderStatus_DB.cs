using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeamonSharps.Shop.Simple.DataBase.Entities
{
    /// <summary>
    /// Статус заказа
    /// </summary>
    [Table("Order_Status")]
    public class OrderStatus_DB
    {
        /// <summary>
        /// Номер статуса
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название статуса
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public List<Order_DB> Orders { get; set; } = new List<Order_DB>();
    }

    public enum OrderStatus 
    {
        Created,
        InProgress,
        Completed
    }

}
