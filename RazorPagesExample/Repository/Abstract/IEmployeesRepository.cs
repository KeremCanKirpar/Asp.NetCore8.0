using System;
using RazorPagesExample.Models;

namespace RazorPagesExample.Repository.Abstract;

public interface IEmployeesRepository
{
    IEnumerable<Employee> GetAll();

    Employee GetbyId(int id);

    Employee Update(Employee entity);
}
