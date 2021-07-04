using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Entities;
using DeamonSharps.Shop.Simple.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order_DB> CreateOrderInDBAsync(IEnumerable<CartItem> products);

        Task<IEnumerable<Order_DB>> GetOrdersAsync();

        Task<IEnumerable<Order_DB>> GetOrdersByPageAsync(int page);

        Task<int> GetPageCountAsync();
    }
}
