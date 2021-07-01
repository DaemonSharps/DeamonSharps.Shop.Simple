﻿using DeamonSharps.Shop.Simple.DataBase.Context;
using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Services
{
    public class OrderServiceController : Controller
    {
        private readonly OrderContext _orderContext;

        private const int PerPage = 14;

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

        /// <summary>
        /// Получение всех заказов из БД
        /// </summary>
        /// <returns>Список заказов</returns>
        public async Task<List<Order>> GetOrders()
        {
            var orders = await _orderContext?.Shop_Orders
                .Include(o => o.Order_Composition)
                .ThenInclude(oc => oc.Product)
                .ToListAsync();
            return orders ?? new List<Order>();
        }

        /// <summary>
        /// Получить заказы по номеру страницы
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <returns>Список заказов</returns>
        public async Task<List<Order>> GetOrdersByPage(int page)
        {
            var orders = new List<Order>();
            var orderFrom = PerPage * (page - 1);
            var orderTo = (page * PerPage) - 1;

            var ordersDB = await GetOrders();

            for (int i = orderFrom; i <= orderTo; i++)
            {
                if (i <= ordersDB.Count -1 )
                {
                    orders.Add(ordersDB.ElementAt(i));
                }
                
            }
            
            return orders ?? new List<Order>();
        }

        /// <summary>
        /// Получить количество страниц для заказов
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetPageCount()
        {
            var orderCount = await Task.Run(()=> 
                {
                    return _orderContext.Shop_Orders.Count(); 
                });

            var pageCount = 0;
            if ((orderCount % PerPage) != 0)
            {
                pageCount = orderCount / PerPage + 1;
            }
            else
            {
                pageCount = orderCount / PerPage;
            }

            return pageCount;
        }
    }
}
