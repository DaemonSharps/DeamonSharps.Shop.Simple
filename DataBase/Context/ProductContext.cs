using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeamonSharps.Shop.Simple.DataBase.Context
{
    public class ProductContext: DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) 
        {
        }
        public DbSet<Entities.Product> Products { get; set; }
    }
}
