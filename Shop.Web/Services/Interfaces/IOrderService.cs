using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order_DB> CreateOrderInDBAsync(IEnumerable<CartItem> products);

        Task<IEnumerable<Order_DB>> GetOrdersByFilterAsync(int page);

        Task<int> GetPageCountAsync();
    }
}
