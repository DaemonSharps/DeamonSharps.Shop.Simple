using System;
using System.Collections.Generic;

namespace DeamonSharps.Shop.Simple.Models
{
    [Serializable]
    public class OrderViewModel
    {
        /// <summary>
        /// Номер заказа
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        public string Status { get; set; }

        public Customer Customer { get; set; } = Customer.GetDefaultSet();

        /// <summary>
        /// Список продуктов в заказе
        /// </summary>
        public List<CartProduct> Products { get; set; }

        /// <summary>
        /// Количество страниц
        /// </summary>
        public int PageCount { get; set; }


        /// <summary>
        /// Текущяя страница
        /// </summary>
        public int CurrentPage { get; set; }
    }

    [Serializable]
    public class Customer
    {
        /// <summary>
        /// Номер пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstNane { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Полное имя
        /// </summary>
        public string FullName => string.Join(" ", FirstNane, SecondName);

        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }

        public static Customer GetDefaultSet()
        {
            return new Customer
            {
                Id = 2,
                FirstNane = "Денис",
                SecondName = "Смирнов",
                Email = "badss@pochta.com"
            };
        }
    }
}
