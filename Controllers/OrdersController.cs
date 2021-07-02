using DeamonSharps.Shop.Simple.Models;
using DeamonSharps.Shop.Simple.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderServiceController _orderServiceController;

        public OrdersController(OrderServiceController orderServiceController)
        {
            _orderServiceController = orderServiceController;
        }
        /// <summary>
        /// Страница для вывода заказов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(int page)
        {
            page = page == 0 ? 1 : page;
            var pageCount = await _orderServiceController.GetPageCountAsync();
            var orders = await _orderServiceController.GetOrdersByPageAsync(page);

            var pageModel = new OrderPageViewModel
            {
                PageCount = pageCount,
                CurrentPage = page,
                Orders = orders.Select(order =>
                new OrderViewModel
                {
                    Id = order.Id,
                    CreationDate = order.Creation_Date,
                    Status = order.Status.Name,
                    Customer = new Customer
                    {
                        FirstName = order.User.First_Name,
                        SecondName = order.User.Second_Name
                    },
                    Products = order.Order_Composition
                    .Where(oc => oc.Order_Id == order.Id)
                    .Select(oc =>
                    new CartProduct
                    {
                        Count = oc.ProductCount,
                        Product = new ProductViewModel
                        {
                            Name = oc.Product.Product_Name,
                            Price = oc.Product.Product_Price,
                            ProductId = oc.Product.Id
                        }
                    }).ToList(),
                    PageCount = pageCount,
                    CurrentPage = page
                }).ToList()
            };

            return View(pageModel);
        }
    }
}
