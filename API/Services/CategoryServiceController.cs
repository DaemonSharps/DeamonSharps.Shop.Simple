using DeamonSharps.Shop.Simple.DataBase.Context;
using DeamonSharps.Shop.Simple.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Api.Services
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoryServiceController : ControllerBase
    {
        private readonly ShopDBContext _shopDBContext;
        public CategoryServiceController(ShopDBContext shopDBContext)
        {
            _shopDBContext = shopDBContext;
        }
        /// <summary>
        /// Получает категории из базы данных
        /// </summary>
        /// <returns>Список категорий</returns>
        [HttpGet("GetCategories")]
        [SwaggerOperation("GetCategories")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<CategoryViewModel>), Description = "Список категорий")]
        public async Task<List<CategoryViewModel>> GetCategoriesFromDBAsync()
        {
            var categories = await _shopDBContext?.Categories?.Select(
            cat => new CategoryViewModel()
            {
                Id = cat.Id,
                Name = cat.Name
            }).ToListAsync();

            return categories;
        }
    }
}
