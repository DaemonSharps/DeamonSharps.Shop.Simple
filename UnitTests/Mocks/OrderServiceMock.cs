using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Services.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaemonSharps.Shop.UnitTests.Mocks
{
    public class OrderServiceMock : Mock<IOrderService>
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
                .Returns(Task.FromResult(CreateOrders(14)));

            Setup(os => os.GetOrdersByFilterAsync(It.Is<int>(p => p == 5)))
                .Returns(Task.FromResult(CreateOrders(4)));
        }

        private IEnumerable<Order_DB> CreateOrders(int count)
        {
            var orders = new List<Order_DB>();
            for (int i = 1; i <= count; i++)
            {
                var order = Order_DB.GetDefaultValue(i);
                order.Status = new OrderStatus_DB
                {
                    Id = order.Status_Id,
                    Name = OrderStatus.Created.ToString()
                };
                order.User = User_DB.GetDefaultValue(order.User_Id);
                order.Order_Composition
                    .ForEach(oc => oc.Product = Product_DB.GetDefaultValue(oc.Product_Id));
                orders.Add(order);
            }
            return orders;
        }
    }
}
