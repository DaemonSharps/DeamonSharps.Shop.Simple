using DeamonSharps.Shop.Simple.DataBase.Context;
using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Services
{
    public class ProductService : IProductService
    {
        private readonly ShopDBContext _shopDBContext;
        private const int PerPage = 8;

        public ProductService(ShopDBContext shopDBContext)
        {
            _shopDBContext = shopDBContext;
        }

        /// <summary>
        /// Получает категории из базы данных
        /// </summary>
        /// <returns>Список категорий</returns>
        public async Task<List<Category_DB>> GetCategoriesFromDBAsync()
        {
            var categories = await _shopDBContext.Categories?.ToListAsync();

            return categories;
        }

        /// <summary>
        /// Получает категорию по её ID
        /// </summary>
        /// <param name="id">Id категории</param>
        /// <returns>Категория продукта</returns>
        public async Task<Category_DB> GetCategoryByIdFromDBAsync(int id)
        {
            return await _shopDBContext.Categories?.SingleAsync(c => c.Id == id);
        }

        /// <summary>
        /// Получает продукт по его ID
        /// </summary>
        /// <param name="id">Id продукта</param>
        /// <returns>Продукт</returns>
        public async Task<Product_DB> GetProductFromDBByIdAsync(int id)
        {
            return await _shopDBContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Получает продукты по списку идентификаторов
        /// </summary>
        /// <param name="ids">Список Id</param>
        /// <returns>Список продуктов</returns>
        public async Task<List<Product_DB>> GetProductsFromDBByIdsAsync(IEnumerable<int> ids)
        {
            return await _shopDBContext.Products.Where(p => ids.Contains(p.Id)).ToListAsync();
        }

        /// <summary>
        /// Получает список продуктов по фильтру
        /// </summary>
        /// <param name="page">Номер страницы в выборке</param>
        /// <param name="categoryId">Номер категории</param>
        /// <returns>Список продуктов</returns>
        public async Task<List<Product_DB>> GetProductsFromDBByFilterAsync(int page, int categoryId = 0)
        {
            if (page <= 0 || categoryId < 0)
            {
                throw new ArgumentException($"Invalid parameters. page: {page}, categoryId: {categoryId}");
            }

            var products = _shopDBContext.Products
                .Include(p => p.ProductCategory)
                .ThenInclude(pc => pc.Category).AsQueryable();
            var from = PerPage * (page - 1);

            if (categoryId != 0)
            {
                products = products.Where(p => p.ProductCategory.Any(pc => pc.Category_Id == categoryId));
            }

            products = products
                .Skip(from)
                .Take(PerPage);

            return await products.ToListAsync();
        }
    }
}
