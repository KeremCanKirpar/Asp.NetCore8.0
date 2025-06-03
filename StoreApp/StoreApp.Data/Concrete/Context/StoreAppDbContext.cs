using System;
using Microsoft.EntityFrameworkCore;
using StoreApp.Web.Models;

namespace StoreApp.Data.Concrete.Context;

public class StoreAppDbContext : DbContext
{
    public StoreAppDbContext(DbContextOptions<StoreAppDbContext> options)
        : base(options)
    {

    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
        .HasMany(c => c.Categories)
        .WithMany(c => c.Products)
        .UsingEntity<ProductCategory>();

        modelBuilder.Entity<Category>()
        .HasIndex(c => c.Url)
        .IsUnique();


        modelBuilder.Entity<Product>().HasData(
            new List<Product>()
            {
                new(){ Id= 1, ProductName = "Samsung S25", Price= 50000, Description =" Yeni Nesil telefon"},
                new(){ Id= 2, ProductName = "Samsung S26", Price= 60000, Description =" Yeni Nesil telefon"},
                new(){ Id= 3, ProductName = "Samsung S27", Price= 70000, Description =" Yeni Nesil telefon"},
                new(){ Id= 4, ProductName = "Samsung S28", Price= 80000, Description =" Yeni Nesil telefon"},
                new(){ Id= 5, ProductName = "Samsung S29", Price= 90000, Description =" Yeni Nesil telefon"},
                new(){ Id= 6, ProductName = "Samsung S30", Price= 100000, Description =" Yeni Nesil telefon"}
            }
        );

        modelBuilder.Entity<Category>().HasData(
            new List<Category>()
            {
                new(){Id = 1, Name ="Telefon", Url = "telefon"},
                new(){Id = 2, Name ="Elektronik", Url = "elektronik"},
                new(){Id = 3, Name ="Beyaz Eşya", Url = "beyaz-esya"},
            }
        );
        modelBuilder.Entity<ProductCategory>().HasData(
                new List<ProductCategory>()
                {
                    new () {ProductId = 1, CategoryId = 1},
                    new () {ProductId = 2, CategoryId = 1},
                    new () {ProductId = 3, CategoryId = 2},
                    new () {ProductId = 4, CategoryId = 2},
                    new () {ProductId = 5, CategoryId = 3},
                    new () {ProductId = 6, CategoryId = 3},

                }
        );
    }
}
