using DeamonSharps.Shop.Simple.DataBase.Context;
using DeamonSharps.Shop.Simple.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<List<ProductViewModel>> GetProductsFromDBAsync()
        {
              var   products= await _productContext.Products
                ?.Select(s=> 
                new ProductViewModel {
                    Name=s.Product_Name ,
                   Price=s.Product_Price,
                   ProductId=s.Id
                })
                ?.ToListAsync();

                return products;
        }
        public async Task<List<ProductViewModel>> GetProductsFromDBByCategoryAsync(int categoryId)
        {
            var products = new List<ProductViewModel>();
            var categories = await _productContext.Categories.Include(cat => cat.ProductCategory).ThenInclude(p => p.Product).ToListAsync();
            foreach (var cat in categories)
            {
                if (cat.Id==categoryId)
                {
                    for (int i = 0; i < cat.ProductCategory.Count; i++)
                    { var prod = cat.ProductCategory[i].Product;
                        products.Add(new ProductViewModel()
                        {
                            Price = prod.Product_Price,
                            ProductId=prod.Id,
                            Name=prod.Product_Name
                        }) ;
                    }
                    break;
                }
            }       
            return products;
        }
    }
}
