using DeamonSharps.Shop.Simple.Entities;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Entites
{
    public class CartTests
    {
        const string CART = "Cart";

        [Fact]
        public void ChangeProductCount_NewProduct()
        {
            //Arrange
            var cart = new Cart();
            var sessionMock = new Mock<ISession>();
            var context = new DefaultHttpContext
            {
                Session = sessionMock.Object
            };

            //Act
            cart.ChangeProductCount(1, 12, context);

            //Assert
            sessionMock.Verify(s => s.Set(
                It.Is<string>(k => k == CART),
                It.IsNotNull<byte[]>()),
                Times.Once);
            Assert.Single(cart.Products);
            Assert.Equal(1, cart.Products[0].ProductId);
            Assert.Equal(12, cart.Products[0].Count);
        }

        [Fact]
        public void ChangeProductCount_ExistedProduct_ZeroCount()
        {
            //Arrange
            var cart = new Cart(
                new CartItem
                {
                    ProductId = 2,
                    Count = 15
                });
            var sessionMock = new Mock<ISession>();
            var context = new DefaultHttpContext
            {
                Session = sessionMock.Object
            };

            //Act
            cart.ChangeProductCount(2, 0, context);

            //Assert
            sessionMock.Verify(s => s.Set(
                It.Is<string>(k => k == CART),
                It.IsNotNull<byte[]>()),
                Times.Exactly(1));
            Assert.Empty(cart.Products);
        }

        [Fact]
        public void ChangeProductCount_ExistedProduct_NewCount()
        {
            const int PROD_ID = 2;
            const int PROD_COUNT = 200;
            const int EXPECTED_COUNT = 300;
            //Arrange
            var cart = new Cart(
                new CartItem
                {
                    ProductId = PROD_ID,
                    Count = PROD_COUNT
                });
            var sessionMock = new Mock<ISession>();
            var context = new DefaultHttpContext
            {
                Session = sessionMock.Object
            };

            //Act
            cart.ChangeProductCount(PROD_ID, EXPECTED_COUNT, context);

            //Assert
            sessionMock.Verify(s => s.Set(
                It.Is<string>(k => k == CART),
                It.IsNotNull<byte[]>()),
                Times.Exactly(1));
            Assert.Single(cart.Products);
            Assert.Equal(PROD_ID, cart.Products[0].ProductId);
            Assert.Equal(EXPECTED_COUNT, cart.Products[0].Count);
        }

        [Fact]
        public void Delete_ExistedItem()
        {
            //Arrange
            const int PROD_ID = 5;
            const int PROD_COUNT = 500;
            const int PROD_ID2 = 6;
            const int PROD_COUNT2 = 70;
            var cart = new Cart(
                new CartItem
                {
                    ProductId = PROD_ID,
                    Count = PROD_COUNT
                },
                new CartItem
                {
                    ProductId = PROD_ID2,
                    Count = PROD_COUNT2
                });
            var sessionMock = new Mock<ISession>();
            var context = new DefaultHttpContext
            {
                Session = sessionMock.Object
            };

            //Act
            cart.Delete(PROD_ID, context);

            //Assert
            Assert.Single(cart.Products);
            Assert.Equal(PROD_ID2, cart.Products[0].ProductId);
            Assert.Equal(PROD_COUNT2, cart.Products[0].Count);
            sessionMock.Verify(s => s.Set(
                It.Is<string>(k => k == CART),
                It.IsNotNull<byte[]>()),
                Times.Once);
        }

        [Fact]
        public void Delete_UnknownItem()
        {
            //Arrange
            const int PROD_ID = 5;
            const int PROD_ID2 = 6;
            const int PROD_COUNT2 = 70;
            var cart = new Cart(new CartItem
            {
                ProductId = PROD_ID2,
                Count = PROD_COUNT2
            });
            var sessionMock = new Mock<ISession>();
            var context = new DefaultHttpContext
            {
                Session = sessionMock.Object
            };

            //Act
            cart.Delete(PROD_ID, context);

            //Assert
            Assert.Single(cart.Products);
            Assert.Equal(PROD_ID2, cart.Products[0].ProductId);
            Assert.Equal(PROD_COUNT2, cart.Products[0].Count);
            sessionMock.Verify(s => s.Set(
                It.Is<string>(k => k == CART),
                It.IsNotNull<byte[]>()),
                Times.Once);
        }

        [Fact]
        public void GetCartItem_Success()
        {
            //Arrange
            const int PROD_ID = 5;
            const int PROD_COUNT = 90;
            const int PROD_ID2 = 6;
            const int PROD_COUNT2 = 70;
            var cart = new Cart(
                new CartItem
                {
                    ProductId = PROD_ID,
                    Count = PROD_COUNT
                },
                new CartItem
                {
                    ProductId = PROD_ID2,
                    Count = PROD_COUNT2
                });

            //Act
            var result = cart.GetCartItem(PROD_ID);

            //Assert
            Assert.Equal(PROD_ID, result.ProductId);
            Assert.Equal(PROD_COUNT, result.Count);
        }

        [Fact]
        public void GetCartItem_EmptyResult()
        {
            //Arrange
            const int PROD_ID2 = 6;
            const int PROD_COUNT2 = 70;
            const int PROD_ID = 5;
            var cart = new Cart(
                new CartItem
                {
                    ProductId = PROD_ID2,
                    Count = PROD_COUNT2
                });

            //Act
            var result = cart.GetCartItem(PROD_ID);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Clean_Success()
        {
            //Arrange
            var sessionMock = new Mock<ISession>();
            var context = new DefaultHttpContext
            {
                Session = sessionMock.Object
            };
            var cart = new Cart();
            for (int i = 0; i < 9; i++)
            {
                cart.Products.Add(
                    new CartItem
                    {
                        ProductId = i,
                        Count = i
                    });
            }

            //Act
            cart.Clean(context);

            //Assert
            sessionMock.Verify(s => s.Set(
                It.Is<string>(k => k == CART),
                It.IsNotNull<byte[]>()),
                Times.Once);
            Assert.Empty(cart.Products);
        }
    }
}
