using DaemonSharps.Shop.UnitTests.Mocks;
using DeamonSharps.Shop.Simple.Controllers;
using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace Controllers
{
    public class OrdersControllerTests
    {
        [Theory]
        [InlineData(1, 14)]
        [InlineData(5, 4)]
        public async Task Index_ReturnCorrectView_And_Model(int page, int count)
        {
            //Arrange
            var orderServiceMock = new OrderServiceMock();
            var controller = new OrdersController(orderServiceMock.Object);
            var expectedStatus = new OrderStatus_DB
            {
                Id = 1,
                Name = OrderStatus.Created.ToString()
            };

            //Act
            var result = await controller.Index(page);

            //Assert
            var view = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<OrderPageViewModel>(view.Model);
            Assert.Equal(page, model.CurrentPage);
            Assert.Equal(OrderServiceMock.PageCount, model.PageCount);
            Assert.Equal(count, model.Orders.Count);
            for (int i = 0; i < model.Orders.Count; i++)
            {
                var order = model.Orders[i];
                Assert.Equal(expectedStatus.Name, order.Status);
                Assert.Single(order.Products);
            }
        }
    }
}
