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
        /// Получить список всех продуктов
        /// </summary>
        /// <returns>Список продуктов</returns>
        public async Task<List<Product_DB>> GetProductsFromDBAsync()
        {
            var products = await _shopDBContext.Products
                .Include(p => p.ProductCategory)
                .ThenInclude(pc => pc.Category)
                .ToListAsync();

            return products;
        }

        /// <summary>
        /// Получить список продуктов по категории
        /// </summary>
        /// <param name="categoryId">Номер категории</param>
        /// <returns>Список продуктов</returns>
        public async Task<List<Product_DB>> GetProductsFromDBByCategoryAsync(int categoryId)
        {

            var category = await _shopDBContext.Categories
                ?.Where(category => category.Id == categoryId)
                .Include(cat => cat.ProductCategory)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync();

            var products = category?.ProductCategory?.Select(pc => pc.Product).ToList();
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


        public async Task<Category_DB> GetCategoryByIdFromDBAsync(int id)
        {
            return await _shopDBContext?.Categories.SingleAsync(c => c.Id == id);
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

        public async Task<List<Product_DB>> GetProductsFromDBByFilterAsync(int page, int categoryId = 0)
        {
            if (page == 0 && categoryId == 0)
            {
                throw new ArgumentException("You must fill in at least one field");
            }

            var products = new List<Product_DB>();
            var productFrom = PerPage * (page - 1);
            var productTo = (page * PerPage) - 1;
            var productsDB = new List<Product_DB>();

            if (categoryId != 0)
            {
                productsDB = await GetProductsFromDBByCategoryAsync(categoryId);
            }
            else
            {
                productsDB = await GetProductsFromDBAsync();
            }

            if (page == 0)
            {
                return productsDB;
            }

            for (int i = productFrom; i <= productTo; i++)
            {
                if (i <= productsDB.Count() - 1)
                {
                    products.Add(productsDB.ElementAt(i));
                }

            }
            return products;
        }
    }
}
