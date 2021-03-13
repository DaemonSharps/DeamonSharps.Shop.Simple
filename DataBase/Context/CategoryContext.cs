using DeamonSharps.Shop.Simple.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeamonSharps.Shop.Simple.DataBase.Context
{
    public class CategoryContext:DbContext
    {
        public CategoryContext(DbContextOptions<CategoryContext> options) : base(options) { }
        public DbSet<Categories> Categories { get; set; }
    }
}
