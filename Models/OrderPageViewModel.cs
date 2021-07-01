using System.Collections.Generic;

namespace DeamonSharps.Shop.Simple.Models
{
    public class OrderPageViewModel
    {
        /// <summary>
        /// Список заказов
        /// </summary>
        public List<OrderViewModel> Orders { get; set; }

        /// <summary>
        /// Текущая страница
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Количество страниц
        /// </summary>
        public int PageCount { get; set; }
    }
}
