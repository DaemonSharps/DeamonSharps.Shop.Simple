using DeamonSharps.Shop.Simple.DataBase.Context;
using DeamonSharps.Shop.Simple.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Services
{
    public class CategoryServiceController : Controller
    {
        private readonly ShopDBContext _shopDBContext;
        public CategoryServiceController(ShopDBContext shopDBContext)
        {
            _shopDBContext = shopDBContext;
        }
        public async Task<List<CategoryViewModel>> GetCategoriesFromDBAsync()
        {
            var categories = await _shopDBContext?.Categories?.Select(
                cat => new CategoryViewModel()
                {
                    Id = cat.Id,
                    Name = cat.Category_Name
                }).ToListAsync();
            return categories;
        }
    }
}
