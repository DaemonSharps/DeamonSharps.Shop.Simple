using System.Collections.Generic;

namespace DeamonSharps.Shop.Simple.DataBase.Entities
{
    public class Role
    {
        /// <summary>
        /// Номер роли
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название роли
        /// </summary>
        public string Role_Name { get; set; }

        /// <summary>
        /// Поле для реализации one-to-many с пользователями
        /// </summary>
        public List<User> Users { get; set; } = new List<User>();
    }
}
