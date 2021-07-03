using DeamonSharps.Shop.Simple.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeamonSharps.Shop.Simple.DataBase.Context
{
    public class ShopDBContext : DbContext
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
    }
}
