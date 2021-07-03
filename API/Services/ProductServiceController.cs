using DeamonSharps.Shop.Simple.DataBase.Context;
using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Models;
using DeamonSharps.Shop.Simple.Services.Interfaces;
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
    public class ProductServiceController : Controller
    {
        private readonly ShopDBContext _shopDBContext;
        private readonly IProductService _productService;
        public ProductServiceController(
            ShopDBContext shopDBContext,
            IProductService productService)
        {
            _shopDBContext = shopDBContext;
            _productService = productService;
        }

        /// <summary>
        /// Получить список всех продуктов
        /// </summary>
        /// <returns>Список продуктов</returns>
        [HttpGet(nameof(GetProductsFromDBAsync))]
        [SwaggerOperation(nameof(GetProductsFromDBAsync))]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<ProductViewModel>))]
        public async Task<List<ProductViewModel>> GetProductsFromDBAsync()
        {
            var products = await _shopDBContext.Products
              ?.Select(s =>
              new ProductViewModel
              {
                  Name = s.Name,
                  Price = s.Price,
                  ProductId = s.Id
              })
              ?.ToListAsync();

            return products;
        }

        /// <summary>
        /// Получить список продуктов по категории
        /// </summary>
        /// <param name="categoryId">Номер категории</param>
        /// <returns>Список продуктов</returns>
        [HttpGet("GetProductsByCategory")]
        [SwaggerOperation("GetProductsByCategory")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Product_DB>))]
        public async Task<IEnumerable<Product_DB>> GetProductsFromDBByCategoryAsync(int categoryId)
        {
            var products = await _productService.GetProductsFromDBByCategoryAsync(categoryId);
            return products;
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
