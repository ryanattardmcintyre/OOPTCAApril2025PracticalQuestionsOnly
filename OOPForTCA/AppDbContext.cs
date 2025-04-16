using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPForTCA
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite Key for OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId });

            // Relationships
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            // Seed Data

            // Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, FullName = "Alice Johnson", Email = "alice@example.com" },
                new Customer { Id = 2, FullName = "Bob Smith", Email = "bob@example.com" }
            );

            // Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 999.99, Stock =10 },
                new Product { Id = 2, Name = "Smartphone", Price = 599.49, Stock=5 },
                new Product { Id = 3, Name = "Headphones", Price = 149.99, Stock=20 }
            );

            // Orders
            modelBuilder.Entity<Order>().HasData(
                   new Order { Id = 1, CustomerId = 1, OrderDate = DateTime.UtcNow.AddDays(-5) },  
                   new Order { Id = 2, CustomerId = 2, OrderDate = DateTime.UtcNow.AddDays(-4) }, 
                   new Order { Id = 3, CustomerId = 1, OrderDate = DateTime.UtcNow.AddDays(-3) },  
                   new Order { Id = 4, CustomerId = 1, OrderDate = DateTime.UtcNow.AddDays(-1) },
                   new Order { Id = 5, CustomerId = 2, OrderDate = DateTime.UtcNow.AddDays(-1) }
                   );


            // Order Items
            modelBuilder.Entity<OrderItem>().HasData(
             
                 new OrderItem { OrderId = 1, ProductId = 1, Quantity = 1 }, 
                 new OrderItem { OrderId = 1, ProductId = 3, Quantity = 2 },

                 
                 new OrderItem { OrderId = 2, ProductId = 2, Quantity = 1 },

              
                 new OrderItem { OrderId = 3, ProductId = 2, Quantity = 2 },

                
                 new OrderItem { OrderId = 4, ProductId = 3, Quantity = 1 },
                 new OrderItem { OrderId = 5, ProductId = 3, Quantity = 1 },
                 new OrderItem { OrderId = 5, ProductId = 2, Quantity = 3 }
             );

        }
    }

}
