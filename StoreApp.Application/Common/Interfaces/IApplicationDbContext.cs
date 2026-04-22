using Microsoft.EntityFrameworkCore;
using StoreApp.Domain.Entities;

namespace StoreApp.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Brand> Brands { get; }
        DbSet<Category> Categories { get; }
        DbSet<Customer> Customers { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }
        DbSet<Product> Products { get; }
        DbSet<Staff> Staffs { get; }
        DbSet<Stock> Stocks { get; }
        DbSet<Store> Stores { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
