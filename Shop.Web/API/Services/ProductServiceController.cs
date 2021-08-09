using DeamonSharps.Shop.Simple.Api.Schemas;
using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IProductService _productService;
        public ProductServiceController(
            IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Получить список продуктов по фильтру
        /// </summary>
        /// <param name="category">Номер категории продуктов</param>
        /// <param name="page">Номер страницы</param>
        /// <returns></returns>
        [HttpGet(nameof(GetProductsByFilter))]
        [SwaggerOperation(nameof(GetProductsByFilter))]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<Product>))]
        public async Task<IActionResult> GetProductsByFilter(int page, int category)
        {
            try
            {
                var productsDB = await _productService.GetProductsFromDBByFilterAsync(page, category);
                if (productsDB == null || productsDB.Count == 0)
                {
                    return BadRequest($"По заданному фильтру не нашлось продуктов");
                }
                var products = await ConvertProductsDBToProducts(productsDB);
                return Ok(products);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Получает категории из базы данных
        /// </summary>
        /// <returns>Список категорий</returns>
        [HttpGet(nameof(GetCategories))]
        [SwaggerOperation(nameof(GetCategories))]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<Category>), Description = "Список категорий")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categoriesDB = await _productService.GetCategoriesFromDBAsync();
                var categories = categoriesDB
                    .Select(c =>
                    new Category
                    {
                        Id = c.Id,
                        Name = c.Name
                    });
                return Ok(categories);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }


        }

        /// <summary>
        /// Преобразовать продукты из БД в продукты для апи
        /// </summary>
        /// <param name="productsDB">Продукты из БД</param>
        /// <returns></returns>
        private async Task<List<Product>> ConvertProductsDBToProducts(List<Product_DB> productsDB)
        {
            var products = new List<Product>();
            foreach (var productDB in productsDB)
            {
                products.Add(
                    new Product
                    {
                        Id = productDB.Id,
                        Name = productDB.Name,
                        Price = productDB.Price,
                        Description = productDB.About,
                        Category = (Category)await _productService
                        .GetCategoryByIdFromDBAsync(productDB.ProductCategory
                        .Single(pc => pc.Product_Id == productDB.Id)
                        .Category_Id)
                    });
            }

            return products;
        }
    }
}
