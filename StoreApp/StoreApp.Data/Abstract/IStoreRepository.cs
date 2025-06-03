using System;
using StoreApp.Data.Concrete.Context;
using StoreApp.Web.Models;

namespace StoreApp.Data.Abstract;

public interface IStoreRepository
{
    IQueryable<Product> Products { get; }
    IQueryable<Category> Categories { get; }

    void CreateProduct(Product product);
    void DeleteProduct(Product product);

    int GetProductCount(string category);

    IEnumerable<Product> GetProductsByCategory(string category, int page, int pageSize);
}
