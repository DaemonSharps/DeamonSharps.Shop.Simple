using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Models;
using DeamonSharps.Shop.Simple.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        /// <summary>
        /// Страница для вывода заказов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(int page)
        {
            page = page == 0 ? 1 : page;
            var pageCount = await _orderService.GetPageCountAsync();
            var orders = await _orderService.GetOrdersByFilterAsync(page);

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
                    Customer = new CustomerViewModel
                    {
                        FirstName = order.User.FirstName,
                        SecondName = order.User.SecondName
                    },
                    Products = order.Order_Composition
                    .Where(oc => oc.Order_Id == order.Id)
                    .Select(oc =>
                    new CartItemViewModel
                    {
                        Count = oc.ProductCount,
                        Product = new ProductViewModel
                        {
                            Name = oc.Product.Name,
                            Price = oc.Product.Price,
                            Id = oc.Product.Id
                        }
                    }).ToList()
                }).ToList()
            };

            return View(pageModel);
        }
    }
}
