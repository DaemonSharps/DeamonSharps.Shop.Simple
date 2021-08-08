using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeamonSharps.Shop.Simple.DataBase.Entities
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    [Table("roles")]
    public class Role_DB
    {
        /// <summary>
        /// Номер роли
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название роли
        /// </summary>
        [Required]
        [Column("Role_Name")]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Поле для реализации one-to-many с пользователями
        /// </summary>
        public List<User_DB> Users { get; set; } = new List<User_DB>();
    }

    public enum UserRoles 
    {
        Admin,
        User
    }

}
