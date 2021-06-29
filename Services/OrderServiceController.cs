using DeamonSharps.Shop.Simple.DataBase.Context;
using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Services
{
    public class OrderServiceController : Controller
    {
        private readonly OrderContext _orderContext;

        public OrderServiceController(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }
        public async Task CreateOrderInDB(IEnumerable<CartProduct> products)
        {
            var order = new Order
            {
                User_Id = 2,
                Creation_Date = DateTime.Now,
                Status_Id = 1
            };
            _orderContext?.Shop_Orders.Add(order);

            for (int i = 0; i < products.Count(); i++)
            {
                order.Order_Composition.Add(new OrderComposition
                {
                    Order_Id = order.Id,
                    Product_Id = products.ElementAt(i).Product.ProductId,
                    ProductCount = products.ElementAt(i).Count
                });
            }

            await _orderContext.SaveChangesAsync();
        }
    }
}
