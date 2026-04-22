using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Common.Interfaces;
using StoreApp.Domain.Entities;

namespace StoreApp.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Stock>().HasNoKey();
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, BrandId = 1, BrandName = "Trek Bikes" },
                new Brand { Id = 2, BrandId = 2, BrandName = "Electra Bikes" },
                new Brand { Id = 3, BrandId = 3, BrandName = "Surly Bikes" }
            );
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryId = 1, CategoryName = "Mountain Bikes" },
                new Category { Id = 2, CategoryId = 2, CategoryName = "Road Bikes" },
                new Category { Id = 3, CategoryId = 3, CategoryName = "Electric Bikes" }
            );
            modelBuilder.Entity<Store>().HasData(
                new Store { Id = 1, StoreId = 1, StoreName = "Santa Cruz Bikes", Phone = "831-476-4321", Email = "santacruz@bikes.com", Street = "3700 Portola Drive", City = "Santa Cruz", State = "CA", ZipCode = "95060" },
                new Store { Id = 2, StoreId = 2, StoreName = "Baldwin Bikes", Phone = "516-379-1000", Email = "baldwin@bikes.com", Street = "4200 Merrick Rd", City = "Massapequa", State = "NY", ZipCode = "11758" }
            );
        }
    }
}
