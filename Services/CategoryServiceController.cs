using DeamonSharps.Shop.Simple.DataBase.Context;
using DeamonSharps.Shop.Simple.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Services
{
    public class CategoryServiceController : Controller
    {
        private readonly CategoryContext _categoryContext;
        public CategoryServiceController(CategoryContext categoryContext)
        {
            _categoryContext = categoryContext;
        }
        public List<CategoryViewModel> GetCategoriesFromDB()
        {
            var categories = _categoryContext?.Categories?.Select(
                cat => new CategoryViewModel()
                {
                    Name = cat.Category_Name
                }).ToList();
            return categories;
        }
    }
}
