using System;
using StoreApp.Data.Concrete.Context;

namespace StoreApp.Web.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }

    public List<Category> Categories { get; set; } = [];
}
