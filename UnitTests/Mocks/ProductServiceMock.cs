using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Services.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaemonSharps.Shop.UnitTests.Mocks
{
    public class ProductServiceMock : Mock<IProductService>
    {
        public ProductServiceMock()
        {
            Set();
        }

        private void Set()
        {
            Setup(ps => ps.GetProductsFromDBByFilterAsync(1, 1))
                .Returns(Task.FromResult(CreateProducts(8)));

            Setup(ps => ps.GetProductsFromDBByFilterAsync(1, 3))
                .Returns(Task.FromResult(CreateProducts(3)));

            Setup(ps => ps.GetCategoriesFromDBAsync())
                .Returns(Task.FromResult(CreateCategories(4)));
        }

        private List<Product_DB> CreateProducts(int count)
        {
            var products = new List<Product_DB>();

            for (int i = 1; i <= count; i++)
            {
                products.Add(Product_DB.GetDefaultValue(i));
            }

            return products;
        }

        private List<Category_DB> CreateCategories(int count)
        {
            var cats = new List<Category_DB>();

            for (int i = 1; i <= count; i++)
            {
                cats.Add(Category_DB.GetDefaultValue(i));
            }

            return cats;
        }
    }
}
