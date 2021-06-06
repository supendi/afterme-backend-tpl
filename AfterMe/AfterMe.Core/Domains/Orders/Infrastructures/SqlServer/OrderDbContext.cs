using AfterMe.Core.Domains.Orders.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace AfterMe.Core.Domains.Orders.Infrastructures.SqlServer
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Get the connection string from system envars instead of from a config file
            string connString = Environment.GetEnvironmentVariable("AfterMe_Constring");
            if (string.IsNullOrEmpty(connString))
                throw new Exception("Connection string has not been set in system envars.");

            optionsBuilder.UseSqlServer(connString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
