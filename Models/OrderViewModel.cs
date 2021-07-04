using System;
using System.Collections.Generic;

namespace DeamonSharps.Shop.Simple.Models
{
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

        public CustomerViewModel Customer { get; set; } = CustomerViewModel.GetDefaultSet();

        /// <summary>
        /// Список продуктов в заказе
        /// </summary>
        public List<CartItemViewModel> Products { get; set; }

        /// <summary>
        /// Количество страниц
        /// </summary>
        public int PageCount { get; set; }


        /// <summary>
        /// Текущяя страница
        /// </summary>
        public int CurrentPage { get; set; }
    }
}
