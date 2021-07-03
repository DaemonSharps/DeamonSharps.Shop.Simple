using DeamonSharps.Shop.Simple.DataBase.Context;
using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Services
{
    public class ProductService : IProductService
    {
        private readonly ShopDBContext _shopDBContext;
        public ProductService(ShopDBContext shopDBContext)
        {
            _shopDBContext = shopDBContext;
        }

        /// <summary>
        /// Получить список всех продуктов
        /// </summary>
        /// <returns>Список продуктов</returns>
        public async Task<List<Product_DB>> GetProductsFromDBAsync()
        {
            var products = await _shopDBContext.Products?.ToListAsync();

            return products;
        }

        /// <summary>
        /// Получить список продуктов по категории
        /// </summary>
        /// <param name="categoryId">Номер категории</param>
        /// <returns>Список продуктов</returns>
        public async Task<List<Product_DB>> GetProductsFromDBByCategoryAsync(int categoryId)
        {

            var caterory = await _shopDBContext.Categories
                ?.Where(category => category.Id == categoryId)
                .Include(cat => cat.ProductCategory)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync();

            var products = caterory.ProductCategory.Select(pc => pc.Product).ToList();
            return products;
        }

        /// <summary>
        /// Получает категории из базы данных
        /// </summary>
        /// <returns>Список категорий</returns>
        public async Task<List<Category_DB>> GetCategoriesFromDBAsync()
        {
            var categories = await _shopDBContext?.Categories?.ToListAsync();

            return categories;
        }

        public async Task<Product_DB> GetProductFromDBByIdAsync(int id)
        {
           return await _shopDBContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product_DB>> GetProductsFromDBByIdsAsync(IEnumerable<int> ids)
        {
            var products = new List<Product_DB>();
            for (int i = 0; i < ids.Count(); i++)
            {
                products.Add(await GetProductFromDBByIdAsync(ids.ElementAt(i)));
            }

            return products;
        }
    }
}
