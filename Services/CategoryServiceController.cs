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
        private readonly ProductContext _productContext;
        public CategoryServiceController(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public List<CategoryViewModel> GetCategoriesFromDB()
        {
            var categories = _productContext?.Categories?.Select(
                cat => new CategoryViewModel()
                { Id=cat.Id,
                    Name = cat.Category_Name
                }).ToList();
            return categories;
        }
    }
}
