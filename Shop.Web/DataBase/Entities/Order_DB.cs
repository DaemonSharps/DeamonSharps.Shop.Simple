using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeamonSharps.Shop.Simple.DataBase.Entities
{
    /// <summary>
    /// Заказ
    /// </summary>
    [Table("Shop_Orders")]
    public class Order_DB
    {
        /// <summary>
        /// Номер заказа в БД
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Номер пользователя из БД
        /// </summary>
        [Required]
        public int User_Id { get; set; }

        /// <summary>
        /// Поле для реализации связи one-to-many с пользователем
        /// </summary>
        public User_DB User { get; set; }

        /// <summary>
        /// Дата создания заказа
        /// </summary>
        [Required]
        [Column("Creation_Date")]
        public DateTime Creation_Date { get; set; }

        /// <summary>
        /// Номер из таблицы статусов
        /// </summary>
        [Required]
        public int Status_Id { get; set; }

        /// <summary>
        /// Поле для реализации one-to-many со статусом
        /// </summary>
        public OrderStatus_DB Status { get; set; }

        /// <summary>
        /// Поле для реализации связи many-to-many
        /// </summary>
        public List<OrderComposition_DB> Order_Composition { get; set; } = new List<OrderComposition_DB>();

        public static Order_DB GetDefaultValue(int index = 1)
        {
            var item = new OrderComposition_DB
            {
                Order_Id = index,
                Product_Id = 1,
                ProductCount = index * 10
            };
            return new Order_DB
            {
                Id = index,
                Creation_Date = DateTime.Today,
                Status_Id = 1,
                User_Id = 2,
                Order_Composition = new List<OrderComposition_DB> { item }
            };
        }

    }
}
