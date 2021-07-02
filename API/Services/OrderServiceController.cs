using DeamonSharps.Shop.Simple.DataBase.Context;
using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Api.Services
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrderServiceController : ControllerBase
    {
        private readonly ShopDBContext _shopDBContext;

        private const int PerPage = 10;

        public OrderServiceController(ShopDBContext shopDBContext)
        {
            _shopDBContext = shopDBContext;
        }

        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <param name="products">Список продуктов в корзине</param>
        /// <returns></returns>
        [HttpPost("CreateOrder")]
        [SwaggerOperation("CreateOrder")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateOrderInDBAsync(IEnumerable<CartProduct> products)
        {
            try
            {
                var order = new Order
                {
                    User_Id = 2,
                    Creation_Date = DateTime.Now,
                    Status_Id = 1
                };
                _shopDBContext?.Shop_Orders.Add(order);

                for (int i = 0; i < products.Count(); i++)
                {
                    order.Order_Composition.Add(new OrderComposition
                    {
                        Order_Id = order.Id,
                        Product_Id = products.ElementAt(i).Product.ProductId,
                        ProductCount = products.ElementAt(i).Count
                    });
                }

                await _shopDBContext.SaveChangesAsync();

                return Ok($"Заказ создан успешно, номер: {order.Id}");
            }
            catch (DbUpdateException e)
            {

                return BadRequest(e.InnerException.Message);
            }
            
        }

        /// <summary>
        /// Получение всех заказов из БД
        /// </summary>
        /// <returns>Список заказов</returns>
        [HttpGet("GetOrders")]
        [SwaggerOperation("GetOrders")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        public async Task<List<Order>> GetOrdersAsync()
        {
            var orders = await _shopDBContext?.Shop_Orders
                .Include(o => o.User)
                .Include(o => o.Status)
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
        [HttpGet("GetOrdersByPage")]
        [SwaggerOperation]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        public async Task<List<Order>> GetOrdersByPageAsync(int page)
        {
            var orders = new List<Order>();
            var orderFrom = PerPage * (page - 1);
            var orderTo = (page * PerPage) - 1;

            var ordersDB = await GetOrdersAsync();

            for (int i = orderFrom; i <= orderTo; i++)
            {
                if (i <= ordersDB.Count - 1)
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
        [HttpGet("GetPageCount")]
        [SwaggerOperation("GetPageCount")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
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
