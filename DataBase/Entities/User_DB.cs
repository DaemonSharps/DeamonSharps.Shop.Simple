using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeamonSharps.Shop.Simple.DataBase.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    [Table("Shop_Users")]
    public class User_DB
    {
        /// <summary>
        /// Номер пользователя
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        [Column("First_Name")]
        [MaxLength(100)]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        [Column("Second_Name")]
        [MaxLength(100)]
        public string SecondName { get; set; }

        /// <summary>
        /// Номер роли
        /// </summary>
        [Required]
        public int Role_Id { get; set; }

        /// <summary>
        /// Поле для реализации one-to-many с ролью
        /// </summary>
        public Role_DB Role { get; set; }

        /// <summary>
        /// Почта
        /// </summary>
        [MaxLength(200)]
        public string Email_Adress { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [MaxLength(100)]
        public string Password { get; set; }

        /// <summary>
        /// Поле для реализации one-to-many с заказом
        /// </summary>
        public List<Order_DB> Orders { get; set; } = new List<Order_DB>();
    }
}
