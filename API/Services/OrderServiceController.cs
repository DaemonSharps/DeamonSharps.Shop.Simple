using DeamonSharps.Shop.Simple.Api.Schemas;
using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Entities;
using DeamonSharps.Shop.Simple.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private readonly IOrderService _orderService;

        public OrderServiceController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <param name="products">Список продуктов в корзине</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateOrder))]
        [SwaggerOperation(nameof(CreateOrder))]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Order))]
        public async Task<IActionResult> CreateOrder([Required][FromBody] IEnumerable<CartItem> products)
        {
            try
            {
                var orderDB = await _orderService.CreateOrderInDBAsync(products);
                var order = ConvertOrdersDBToOrders(orderDB);
                return Ok(order.FirstOrDefault());
            }
            catch (DbUpdateException e)
            {

                return BadRequest(e.InnerException.Message);
            }

        }

        /// <summary>
        /// Получить заказы по номеру страницы
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <returns>Список заказов</returns>
        [HttpGet(nameof(GetOrdersByFilter))]
        [SwaggerOperation(nameof(GetOrdersByFilter))]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<Order>))]
        public async Task<IActionResult> GetOrdersByFilter(int page)
        {
            try
            {
                var ordersDB = await _orderService.GetOrdersByFilterAsync(page);
                var orders = ConvertOrdersDBToOrders(ordersDB.ToArray());

                return Ok(orders);

            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Получить количество страниц для заказов
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetPageCount))]
        [SwaggerOperation(nameof(GetPageCount))]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(int))]
        public async Task<IActionResult> GetPageCount()
        {
            try
            {
                var pageCount = await _orderService.GetPageCountAsync();
                return Ok(pageCount);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private List<Order> ConvertOrdersDBToOrders(params Order_DB[] ordersDB)
        {
            var orders = new List<Order>();
            foreach (var orderDB in ordersDB)
            {
                /*var user = new User
                {
                    Id = orderDB.User_Id,
                    FirstName = orderDB.User.FirstName,
                    SecondName = orderDB.User.SecondName,
                    Email_Adress = orderDB.User.Email_Adress,
                    Role = new Role
                    {
                        Id = orderDB.User.Role_Id,
                        Name = orderDB.User.Role.Name
                    }
                };*/
                var order = new Order
                {
                    Id = orderDB.Id,
                    Creation_Date = orderDB.Creation_Date,
                    Status = orderDB.Status.Name
                };
                orders.Add(order);
            }

            return orders;
        }
    }
}
