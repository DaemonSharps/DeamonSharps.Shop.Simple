﻿using DeamonSharps.Shop.Simple.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeamonSharps.Shop.Simple.DataBase.Context
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Shop_Orders { get; set; }

        public DbSet<OrderStatus> OrderStatus { get; set; }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(order => order.Status)
                .WithMany(status => status.Orders)
                .HasForeignKey(order => order.Status_Id);

            modelBuilder.Entity<Order>()
                .HasOne(order => order.User)
                .WithMany(user => user.Orders)
                .HasForeignKey(order => order.User_Id);

            //many-to-many link in EF version < 5
            modelBuilder.Entity<OrderComposition>()
                .HasKey(key => new { key.Order_Id, key.Product_Id });

            modelBuilder.Entity<OrderComposition>()
                ?.HasOne(o => o.Order)
                ?.WithMany(m => m.Order_Composition)
                ?.HasForeignKey(fk => fk.Order_Id);

            modelBuilder.Entity<OrderComposition>()
                ?.HasOne(o => o.Product)
                ?.WithMany(m => m.Order_Composition)
                ?.HasForeignKey(fk => fk.Product_Id);
        }
    }
}