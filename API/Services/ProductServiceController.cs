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
    public class ProductServiceController : Controller
    {
        private readonly ShopDBContext _shopDBContext;
        public ProductServiceController(ShopDBContext shopDBContext)
        {
            _shopDBContext = shopDBContext;
        }

        /// <summary>
        /// Получить список всех продуктов
        /// </summary>
        /// <returns>Список продуктов</returns>
        [HttpGet("GetProducts")]
        [SwaggerOperation("GetProducts")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<ProductViewModel>))]
        public async Task<List<ProductViewModel>> GetProductsFromDBAsync()
        {
            var products = await _shopDBContext.Products
              ?.Select(s =>
              new ProductViewModel
              {
                  Name = s.Product_Name,
                  Price = s.Product_Price,
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
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<ProductViewModel>))]
        public async Task<List<ProductViewModel>> GetProductsFromDBByCategoryAsync(int categoryId)
        {
            var products = new List<ProductViewModel>();
            var categories = await _shopDBContext.Categories.Include(cat => cat.ProductCategory).ThenInclude(p => p.Product).ToListAsync();
            foreach (var cat in categories)
            {
                if (cat.Id == categoryId)
                {
                    for (int i = 0; i < cat.ProductCategory.Count; i++)
                    {
                        var prod = cat.ProductCategory[i].Product;
                        products.Add(new ProductViewModel()
                        {
                            Price = prod.Product_Price,
                            ProductId = prod.Id,
                            Name = prod.Product_Name
                        });
                    }
                    break;
                }
            }
            return products;
        }
    }
}
