using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Services.Interfaces
{
    public interface IOrderService
    {
        Task<int> CreateOrderInDBAsync(IEnumerable<CartProduct> products);

        Task<IEnumerable<Order_DB>> GetOrdersAsync();

        Task<IEnumerable<Order_DB>> GetOrdersByPageAsync(int page);

        Task<int> GetPageCountAsync();
    }
}
