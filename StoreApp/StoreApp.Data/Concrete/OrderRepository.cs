using System;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete.Context;

namespace StoreApp.Data.Concrete;

public class OrderRepository : IOrderRepository
{
    private StoreAppDbContext _context;

    public OrderRepository(StoreAppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Order> Orders => _context.Orders;

    public void SaveOrder(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }
}
