using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeamonSharps.Shop.Simple.DataBase.Entities
{
    public class Order
    {
        /// <summary>
        /// Номер заказа в БД
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Номер пользователя из БД
        /// </summary>
        public int User_Id { get; set; }

        /// <summary>
        /// Дата создания заказа
        /// </summary>
        [Column("Creation_Date")]
        public DateTime Creation_Date { get; set; }

        /// <summary>
        /// Номер из таблицы статусов
        /// </summary>
        public int Status_Id { get; set; }

        /// <summary>
        /// Поле для реализации связи many-to-many
        /// </summary>
        public List<OrderComposition> Order_Composition { get; set; } = new List<OrderComposition>();

    }
}
