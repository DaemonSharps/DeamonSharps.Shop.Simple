using DeamonSharps.Shop.Simple.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeamonSharps.Shop.Simple.DataBase.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.Role_Id);
        }
    }
}
