using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Models
{
    public struct CustomerViewModel
    {
        /// <summary>
        /// Номер пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Полное имя
        /// </summary>
        public string FullName => string.Join(" ", FirstName, SecondName);

        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }

        public static CustomerViewModel GetDefaultSet()
        {
            return new CustomerViewModel
            {
                Id = 2,
                FirstName = "Денис",
                SecondName = "Смирнов",
                Email = "badss@pochta.com"
            };
        }
    }
}
