using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Services.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaemonSharps.Shop.UnitTests.Mocks
{
    public class OrderServiceMock: Mock<IOrderService>
    {
        public static readonly int PageCount = 5;
        public OrderServiceMock()
        {
            Set();
        }

        private void Set()
        {
            Setup(os => os.GetPageCountAsync()).Returns(() => Task.FromResult(PageCount));

            Setup(os => os.GetOrdersByFilterAsync(It.Is<int>(p => p == 1)))
                .Returns(async () => 
                {
                    var orders = new List<Order_DB>();
                    for (int i = 0; i < 14; i++)
                    {
                        orders.Add(Order_DB.GetDefaultValue(i));
                    }
                    return orders;
                } );

            Setup(os => os.GetOrdersByFilterAsync(It.Is<int>(p => p == 5)))
                .Returns(async () =>
                {
                    var orders = new List<Order_DB>();
                    for (int i = 0; i < 4; i++)
                    {
                        orders.Add(Order_DB.GetDefaultValue(i));
                    }
                    return orders;
                });
        }

    }
}
