using DaemonSharps.Shop.UnitTests;
using DeamonSharps.Shop.Simple.DataBase.Context;
using DeamonSharps.Shop.Simple.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Services
{
    public class ProductServiceTests: DBTestBase<ShopDBContext>
    {
        public ProductServiceTests() 
            : base(new DbContextOptionsBuilder<ShopDBContext>()
                  .UseMySql(TestParameters.GetConnectionString("R4VySZ3Qpq", "SuEOrDOZd1"))
                  .Options) { }

        [Fact]
        public async Task GetCategoriesFromDBAsync_Succes()
            => await WithDBContextAsync(async(context) => 
            {
                //Arrange
                var service = new ProductService(context);

                //Act
                var result = await service.GetCategoriesFromDBAsync();

                //Assert
                Assert.Equal(4, result.Count);
            });

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task GetCategoryByIdFromDBAsync_Success(int id)
            => await WithDBContextAsync(async (context) =>
            {
                //Arrange
                var service = new ProductService(context);

                //Act
                var result = await service.GetCategoryByIdFromDBAsync(id);

                //Assert
                Assert.NotNull(result);
                Assert.Equal(id,result.Id);
                Assert.Equal($"name{id}", result.Name);
            });

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(8)]
        public async Task GetProductFromDBByIdAsync_Success(int id)
            => await WithDBContextAsync(async (context) =>
            {
                //Arrange
                var service = new ProductService(context);

                //Act
                var result = await service.GetProductFromDBByIdAsync(id);

                //Assert
                Assert.NotNull(result);
                Assert.Equal(id, result.Id);
                Assert.Equal($"name{id}", result.Name);
                Assert.Equal($"about{id}", result.About);
                Assert.Equal(id, result.Price);
            });

        [Fact]
        public async Task GetProductsFromDBByIdsAsync_Success()
            => await WithDBContextAsync(async (context) =>
            {
                //Arrange
                var service = new ProductService(context);
                var ids = new[] {1,5,8 };

                //Act
                var result = await service.GetProductsFromDBByIdsAsync(ids);

                //Assert
                Assert.Equal(3, result.Count);
                for (int i = 0; i < ids.Length; i++)
                {
                    Assert.Equal(ids[i], result[i].Id);
                    Assert.Equal($"name{ids[i]}", result[i].Name);
                    Assert.Equal($"about{ids[i]}", result[i].About);
                    Assert.Equal(ids[i], result[i].Price);
                }
            });

        [Theory]
        [InlineData(1, 8)]
        [InlineData(10, 8)]
        [InlineData(1, 8, 1)]
        [InlineData(2, 8, 2)]
        [InlineData(3, 4, 3)]
        [InlineData(4, 0, 4)]
        public async Task GetProductsFromDBByFilterAsync_Success(int page, int count, int categoryId = 0)
            => await WithDBContextAsync(async (context) =>
            {
                //Arrange
                var service = new ProductService(context);

                //Act
                var result = await service.GetProductsFromDBByFilterAsync(page, categoryId);

                //Assert
                Assert.Equal(count, result.Count);
                if (categoryId != 0)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        Assert.Contains(result[i].ProductCategory, pc => pc.Category_Id == categoryId);
                    }
                }
            });

        [Theory]
        [InlineData(0)]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]

        public async Task GetProductsFromDBByFilterAsync_ZeroPage_Exception(int page, int categoryId = 0)
            => await WithDBContextAsync(async (context) =>
            {
                //Arrange
                var service = new ProductService(context);

                //Act
                Func<Task> act = ()=> service.GetProductsFromDBByFilterAsync(page, categoryId);

                //Assert
                var exception = await Assert.ThrowsAsync<ArgumentException>(act);
                Assert.Equal($"Invalid parameters. page: {page}, categoryId: {categoryId}", exception.Message);
            });
    }
}
