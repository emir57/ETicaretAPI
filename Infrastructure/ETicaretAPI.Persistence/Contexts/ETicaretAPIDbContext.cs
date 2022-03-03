using ETicaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Persistence.Contexts
{
    public class ETicaretAPIDbContext : DbContext
    {
        public ETicaretAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().Ignore(x => x.Orders);
            //modelBuilder.Entity<Order>().Ignore(x => x.Products);
            modelBuilder.Entity<OrderProduct>()
                .HasKey(x => new { x.ProductId, x.OrderId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
