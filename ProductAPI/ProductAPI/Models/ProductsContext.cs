using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Models;

public class ProductsContext : IdentityDbContext<AppUser,AppRole, int>
{

    public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
    {
       
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().HasData(
            new Product { ProductId = 1, ProductName = "Iphone 14", Price = 60000, IsActive = true },
            new Product { ProductId = 2, ProductName = "Iphone 15", Price = 70000, IsActive = true },
            new Product { ProductId = 3, ProductName = "Iphone 16", Price = 80000, IsActive = true },
            new Product { ProductId = 4, ProductName = "Iphone 17", Price = 90000, IsActive = true },
            new Product { ProductId = 5, ProductName = "Iphone 18", Price = 100000, IsActive = true }
        );
    }

    public DbSet<Product> Product { get; set; }
}
