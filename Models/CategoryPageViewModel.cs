using System.Collections.Generic;

namespace DeamonSharps.Shop.Simple.Models
{
    /// <summary>
    /// Модель страницы с категориями
    /// </summary>
    public class CategoryPageViewModel
    {

        public string Title { get; set; } = "Категории";
        public List<Category> Categories { get; set; }
    }

    public struct Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
