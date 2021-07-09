using System.Collections.Generic;

namespace DeamonSharps.Shop.Simple.Models
{
    /// <summary>
    /// Модель страницы с категориями
    /// </summary>
    public class CategoryPageViewModel
    {

        public string Title { get; set; } = "Категории";
        public List<CategoryViewModel> Categories { get; set; }
    }
}
