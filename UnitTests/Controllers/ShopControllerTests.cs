using DaemonSharps.Shop.UnitTests.Mocks;
using DeamonSharps.Shop.Simple.Controllers;
using DeamonSharps.Shop.Simple.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Controllers
{
    public class ShopControllerTests
    {
        [Theory]
        [InlineData(1, 8)]
        [InlineData(3, 3)]
        public async Task Index_Success(int categoryId, int productsCount)
        {
            //Arrange
            var productServiceMock = new ProductServiceMock();
            var cookieProviderMock = new CookieProviderMock(categoryId);
            var sessionMock = new SessionMock();
            var context = new DefaultHttpContext
            {
                Session = sessionMock.Object
            };
            var controller = new ShopController(productServiceMock.Object, cookieProviderMock.Object) 
            {
                ControllerContext = new ControllerContext() { HttpContext = context }
            };

            //Act
            var result = await controller.Index();

            //Assert
            var view = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ShopPageViewModel>(view.Model);
            Assert.Equal(productsCount, model.Products.Count);
            Assert.Equal(4, model.Categories.Count);
            Assert.Equal(categoryId, model.CategoryId);
        }
    }
}
