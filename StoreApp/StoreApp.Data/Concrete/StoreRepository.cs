using System;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete.Context;
using StoreApp.Web.Models;

namespace StoreApp.Data.Concrete;

public class StoreRepository : IStoreRepository
{
    private readonly StoreAppDbContext _context;

    public StoreRepository(StoreAppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Product> Products => _context.Products;
    public IQueryable<Category> Categories => _context.Categories;

    public void CreateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public void DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }

    public int GetProductCount(string category)
    {
        return category == null ? Products.Count() :
                        Products
                        .Include(p => p.Categories)
                        .Where(p => p.Categories
                        .Any(a => a.Url == category)).Count();
    }

    public IEnumerable<Product> GetProductsByCategory(string category, int page, int pageSize)
    {
        var products = Products;
        if (!string.IsNullOrEmpty(category))
        {
            products = products.Include(p => p.Categories).Where(p => p.Categories.Any(a => a.Url == category));
        }
        return products = products.Skip((page - 1) * pageSize).Take(pageSize);

    }
}
