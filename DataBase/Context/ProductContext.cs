using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeamonSharps.Shop.Simple.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeamonSharps.Shop.Simple.DataBase.Context
{
    public class ProductContext: DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) {}
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //many-to-many link in EF version < 5
            modelBuilder.Entity<ProductCategory>()
            .HasKey(t => new { t.Category_Id, t.Product_Id});

            modelBuilder.Entity<ProductCategory>()
                .HasOne(cc=>cc.Category)
                .WithMany(c=>c.ProductCategory)
                .HasForeignKey(cc=>cc.Category_Id);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc=>pc.Product)
                .WithMany(p=>p.ProductCategory)
                .HasForeignKey(pc=>pc.Product_Id);
        }
    }
}
