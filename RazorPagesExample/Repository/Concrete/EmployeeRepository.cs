using System;
using RazorPagesExample.Models;
using RazorPagesExample.Models.Context;
using RazorPagesExample.Repository.Abstract;

namespace RazorPagesExample.Repository.Concrete;

public class EmployeeRepository : IEmployeesRepository
{
    private readonly DataContext _context;

    public EmployeeRepository(DataContext context)
    {
        _context = context;
    }

    public IEnumerable<Employee> GetAll()
    {
        return _context.Employees.ToList();
    }

    public Employee GetbyId(int id)
    {
        return _context.Employees.FirstOrDefault(i => i.Id == id);
    }

    public Employee Update(Employee entity)
    {
        var response = _context.Employees.FirstOrDefault(i => i.Id == entity.Id);

        if (response != null)
        {
            response.Name = entity.Name;
            response.Email = entity.Email;
            response.Department = entity.Department;
            response.Photo = entity.Photo;
            _context.SaveChanges();
        }
        return response;
    }
}
