﻿using DeamonSharps.Shop.Simple.DataBase.Context;
using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Entities;
using DeamonSharps.Shop.Simple.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Services
{
    public class OrderService : IOrderService
    {
        private readonly ShopDBContext _shopDBContext;

        private const int PerPage = 14;
        public OrderService(ShopDBContext shopDBContext)
        {
            _shopDBContext = shopDBContext;
        }

        /// <summary>
        /// Создание заказа в базе данных
        /// </summary>
        /// <param name="products">Список продуктов в корзине</param>
        /// <returns></returns>
        public async Task<Order_DB> CreateOrderInDBAsync(IEnumerable<CartItem> products)
        {
            if (products == null || products.Count() == 0)
            {
                throw new ArgumentException("Products count should be more than thero.");
            }

            var order = new Order_DB
            {
                User_Id = 2,
                Creation_Date = DateTime.Now,
                Status_Id = 1
            };
            _shopDBContext.Shop_Orders.Add(order);

            for (int i = 0; i < products.Count(); i++)
            {
                order.Order_Composition.Add(new OrderComposition_DB
                {
                    Order_Id = order.Id,
                    Product_Id = products.ElementAt(i).ProductId,
                    ProductCount = products.ElementAt(i).Count
                });
            }

            await _shopDBContext.SaveChangesAsync();

            order.Status = await _shopDBContext.OrderStatus.Where(s => s.Id == order.Status_Id).SingleAsync();
            order.User = await _shopDBContext.Users.Where(u => u.Id == order.User_Id).SingleAsync();
            return order;
        }

        /// <summary>
        /// Получить заказы по номеру страницы
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <returns>Список заказов</returns>
        public async Task<IEnumerable<Order_DB>> GetOrdersByFilterAsync(int page)
        {
            if (page == 0)
            {
                throw new ArgumentException("Page is can`t be 0");
            }
            var orderFrom = PerPage * (page - 1);

            var orders = await _shopDBContext.Shop_Orders
                .Skip(orderFrom)
                .Take(PerPage)
                .Include(o => o.User)
                .Include(o => o.Status)
                .Include(o => o.Order_Composition)
                .ThenInclude(oc => oc.Product)
                .ToListAsync();

            return orders ?? new List<Order_DB>();
        }

        /// <summary>
        /// Получить количество страниц для заказов
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetPageCountAsync()
        {
            var orderCount = await Task.Run(() =>
            {
                return _shopDBContext.Shop_Orders.Count();
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
