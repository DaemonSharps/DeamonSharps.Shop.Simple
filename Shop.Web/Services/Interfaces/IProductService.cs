using DeamonSharps.Shop.Simple.DataBase.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Services.Interfaces
{
    public interface IProductService
    {
        public Task<List<Product_DB>> GetProductsFromDBAsync();

        public Task<List<Product_DB>> GetProductsFromDBByCategoryAsync(int categoryId);

        public Task<List<Product_DB>> GetProductsFromDBByFilterAsync(int page, int categoryId);

        public Task<List<Category_DB>> GetCategoriesFromDBAsync();

        public Task<Category_DB> GetCategoryByIdFromDBAsync(int id);

        public Task<Product_DB> GetProductFromDBByIdAsync(int id);

        public Task<List<Product_DB>> GetProductsFromDBByIdsAsync(IEnumerable<int> ids);
    }
}
