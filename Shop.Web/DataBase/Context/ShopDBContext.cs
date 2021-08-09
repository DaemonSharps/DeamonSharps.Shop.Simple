using DeamonSharps.Shop.Simple.DataBase.Entities;
using DeamonSharps.Shop.Simple.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DeamonSharps.Shop.Simple.DataBase.Context
{
    public class ShopDBContext : DbContext, IDefaultValue<List<object>>
    {
        public ShopDBContext(DbContextOptions<ShopDBContext> options) : base(options) { }

        public DbSet<Product_DB> Products { get; set; }

        public DbSet<Order_DB> Shop_Orders { get; set; }

        public DbSet<OrderStatus_DB> OrderStatus { get; set; }

        public DbSet<User_DB> Users { get; set; }

        public DbSet<Role_DB> Roles { get; set; }

        public DbSet<Category_DB> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order_DB>()
                .HasOne(order => order.Status)
                .WithMany(status => status.Orders)
                .HasForeignKey(order => order.Status_Id);

            modelBuilder.Entity<Order_DB>()
                .HasOne(order => order.User)
                .WithMany(user => user.Orders)
                .HasForeignKey(order => order.User_Id);

            modelBuilder.Entity<User_DB>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.Role_Id);

            //many-to-many link in EF version < 5
            modelBuilder.Entity<OrderComposition_DB>()
                .HasKey(key => new { key.Order_Id, key.Product_Id });

            modelBuilder.Entity<OrderComposition_DB>()
                ?.HasOne(o => o.Order)
                ?.WithMany(m => m.Order_Composition)
                ?.HasForeignKey(fk => fk.Order_Id);

            modelBuilder.Entity<OrderComposition_DB>()
                ?.HasOne(o => o.Product)
                ?.WithMany(m => m.Order_Composition)
                ?.HasForeignKey(fk => fk.Product_Id);


            //many-to-many link in EF version < 5
            modelBuilder.Entity<ProductCategory_DB>()
            .HasKey(t => new { t.Category_Id, t.Product_Id });

            modelBuilder.Entity<ProductCategory_DB>()
                .HasOne(cc => cc.Category)
                .WithMany(c => c.ProductCategory)
                .HasForeignKey(cc => cc.Category_Id);

            modelBuilder.Entity<ProductCategory_DB>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategory)
                .HasForeignKey(pc => pc.Product_Id);
        }

        public List<object> GetDefaultValue()
        {
            var seed = new List<object>();
            var categories = new List<Category_DB>();
            for (int i = 1; i < 5; i++)
            {
                var cat = Category_DB.GetDefaultValue(i);
                categories.Add(cat);
            }

            var products = new List<Product_DB>();
            for (int i = 1; i <= categories.Count * 20; i++)
            {
                var prod = Product_DB.GetDefaultValue(i);
                products.Add(prod);
            }

            var productCategory = new List<ProductCategory_DB>();
            for (int i = 0; i < categories.Count; i++)
            {
                var from = i * 20;
                var to = (i + 1) * 20;
                for (int j = from; j < to; j++)
                {
                    var prodCat = new ProductCategory_DB
                    {
                        Category_Id = categories[i].Id,
                        Product_Id = products[j].Id
                    };

                    productCategory.Add(prodCat);
                }
            }
            var statuses = new List<OrderStatus_DB>
            {
                new OrderStatus_DB
                {
                    Id = 1,
                    Name = Entities.OrderStatus.Created.ToString()
                },
                new OrderStatus_DB
                {
                    Id = 2,
                    Name = Entities.OrderStatus.InProgress.ToString()
                },
                new OrderStatus_DB
                {
                    Id = 3,
                    Name = Entities.OrderStatus.Completed.ToString()
                }
            };

            var users = new List<User_DB>();
            for (int i = 1; i < 5; i++)
            {
                var user = User_DB.GetDefaultValue(i);
                user.Role_Id = 1;
                users.Add(user);
            }
            var roles = new List<Role_DB>
            {
                new Role_DB
                {
                    Id = 1,
                    Name = UserRoles.Admin.ToString()
                },
                new Role_DB
                {
                    Id = 2,
                    Name = UserRoles.User.ToString()
                }
            };
            seed.AddRange(categories);
            seed.AddRange(products);
            seed.AddRange(productCategory);
            seed.AddRange(statuses);
            seed.AddRange(users);
            seed.AddRange(roles);
            return seed;
        }
    }
}
