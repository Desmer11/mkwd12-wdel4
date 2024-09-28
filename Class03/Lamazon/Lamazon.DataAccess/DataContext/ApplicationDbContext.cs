using Lamazon.DataAccess.Extensions;
using Lamazon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Lamazon.DataAccess.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.SeedProducts()
                        .SeedProductCategory()
                        .SeedProductCategoryStatus()
                        .SeedProductStatus();
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }
        public DbSet<InvoiceStatus> InvoiceStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLineItem> OrdersLineItems { get; set; }
        public DbSet<OrderStatus> OrdersStatuses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStatus> ProductStatuses { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductCategoryStatus> ProductCategoryStatuses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
