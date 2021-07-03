using DeamonSharps.Shop.Simple.DataBase.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Services.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<Product_DB>> GetProductsFromDBAsync();

        public Task<IEnumerable<Product_DB>> GetProductsFromDBByCategoryAsync(int categoryId);

        public Task<IEnumerable<Category_DB>> GetCategoriesFromDBAsync();
    }
}
