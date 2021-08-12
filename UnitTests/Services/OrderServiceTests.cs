using DaemonSharps.Shop.UnitTests;
using DeamonSharps.Shop.Simple.DataBase.Context;
using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Entities;
using DeamonSharps.Shop.Simple.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Services
{
    public class OrderServiceTests : DBTestBase<ShopDBContext>
    {
        public OrderServiceTests()
            : base(new DbContextOptionsBuilder<ShopDBContext>()
                  .UseMySql(TestParameters.GetConnectionString("M5UEG6d04a", "zsIkBXfF0Y"))
                  .Options)
        { }

        [Fact]
        public async Task CreateOrderInDBAsync_Success()
            => await WithDBContextAsync(async (context) =>
            {
                //Arrange
                var cartItems = new List<CartItem>
                {
                    new CartItem
                    {
                        ProductId = 1,
                        Count = 20
                    },
                    new CartItem
                    {
                        ProductId = 2,
                        Count = 30
                    },
                    new CartItem
                    {
                        ProductId = 3,
                        Count = 10
                    }
                };
                var service = new OrderService(context);

                //Act
                var order = await service.CreateOrderInDBAsync(cartItems);

                //Assert
                foreach (var cartItem in cartItems)
                {
                    var expectedComposition = order.Order_Composition.FirstOrDefault(c => c.Product_Id == cartItem.ProductId);
                    Assert.NotNull(expectedComposition);
                    Assert.Equal(cartItem.Count, expectedComposition.ProductCount);
                }

                Assert.Equal(1, order.Status_Id);
                Assert.Equal(order.Status.Id, order.Status_Id);
                Assert.Equal(OrderStatus.Created.ToString(), order.Status.Name);

                Assert.Equal(2, order.User_Id);
                Assert.Equal(order.User.Id, order.User_Id);

                var expectedOrder = context.Shop_Orders.FirstOrDefault(o => o.Id == order.Id);
                Assert.NotNull(expectedOrder);
                Assert.Equal(expectedOrder, order);

            });

        [Fact]
        public async Task CreateOrderInDBAsync_NullItems_Exception()
            => await WithDBContextAsync(async (context) =>
            {
                //Arrange
                var service = new OrderService(context);

                //Act
                Func<Task> act = () => service.CreateOrderInDBAsync(null);

                //Assert
                var exception = await Assert.ThrowsAsync<ArgumentException>(act);
                Assert.Equal("Products count should be more than thero.", exception.Message);
            });

        [Fact]
        public async Task CreateOrderInDBAsync_ZeroCount_Exception()
            => await WithDBContextAsync(async (context) =>
            {
                //Arrange
                var items = new List<CartItem>();
                var service = new OrderService(context);

                //Act
                Func<Task> act = () => service.CreateOrderInDBAsync(null);

                //Assert
                var exception = await Assert.ThrowsAsync<ArgumentException>(act);
                Assert.Equal("Products count should be more than thero.", exception.Message);
            });

        [Fact]
        public async Task GetPageCountAsync_Success()
            => await WithDBContextAsync(async (context) =>
            {
                //Arrange
                var orders = new List<Order_DB>();
                for (int i = 1; i < 500; i++)
                {
                    orders.Add(Order_DB.GetDefaultValue(i));
                }
                await context.Shop_Orders.AddRangeAsync(orders);
                await context.SaveChangesAsync();
                var service = new OrderService(context);

                //Act
                var count = await service.GetPageCountAsync();

                //Assert
                Assert.Equal(36, count);
            });

        [Theory]
        [InlineData(1)]
        [InlineData(12)]
        [InlineData(32)]
        public async Task GetOrdersByFilterAsync_Success(int page)
            => await WithDBContextAsync(async (context) =>
            {
                //Arrange
                var orders = new List<Order_DB>();
                for (int i = 1; i < 500; i++)
                {
                    orders.Add(Order_DB.GetDefaultValue(i));
                }
                await context.Shop_Orders.AddRangeAsync(orders);
                await context.SaveChangesAsync();
                var service = new OrderService(context);

                //Act
                var result = await service.GetOrdersByFilterAsync(page);

                //Assert
                Assert.NotNull(result);
                Assert.Equal(14, result.Count());
            });

        [Fact]
        public async Task GetOrdersByFilterAsync_ZeroPage_Exception()
            => await WithDBContextAsync(async (context) =>
            {
                //Arrange
                var orders = new List<Order_DB>();
                for (int i = 1; i < 500; i++)
                {
                    orders.Add(Order_DB.GetDefaultValue(i));
                }
                await context.Shop_Orders.AddRangeAsync(orders);
                await context.SaveChangesAsync();
                var service = new OrderService(context);

                //Act
                Func<Task> act = () => service.GetOrdersByFilterAsync(0);

                //Assert
                var exception = await Assert.ThrowsAsync<ArgumentException>(act);
                Assert.Equal("Page is can`t be 0", exception.Message);
            });
    }
}
