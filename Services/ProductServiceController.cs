using DeamonSharps.Shop.Simple.DataBase.Context;
using DeamonSharps.Shop.Simple.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.Services
{
    public class ProductServiceController : Controller
    {
        private readonly ProductContext _productContext;
        public ProductServiceController(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public List<ProductViewModel> GetProductsFromDB()
        {
              var   products=_productContext.products
                ?.Select(s=> 
                new ProductViewModel {
                    Name=s.Product_Name ,
                   Price=s.Product_Price,
                   ProductId=s.Id
                })
                ?.ToList();
                return products;
        }
    }
}
