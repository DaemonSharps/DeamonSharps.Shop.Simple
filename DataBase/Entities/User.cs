using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeamonSharps.Shop.Simple.DataBase.Entities
{
    [Table("Shop_Users")]
    public class User
    {
        /// <summary>
        /// Номер пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string First_Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Second_Name { get; set; }

        /// <summary>
        /// Номер роли
        /// </summary>
        public int Role_Id { get; set; }

        /// <summary>
        /// Поле для реализации one-to-many с ролью
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Почта
        /// </summary>
        public string Email_Adress { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Поле для реализации one-to-many с заказом
        /// </summary>
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
