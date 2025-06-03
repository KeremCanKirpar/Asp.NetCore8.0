using System;
using RazorPagesExample.Models;
using RazorPagesExample.Repository.Abstract;

namespace RazorPagesExample.Repository.Concrete;

public class MockEmployeeRepository : IEmployeesRepository
{
    private List<Employee> _employeeList;

    public MockEmployeeRepository()
    {
        _employeeList = new List<Employee>()
        {
            new() {Id = 1, Name = "Kerem Can Kırpar", Email = "info@keremcan.com", Photo= "45.jpg", Department = "Yazılım"},
            new() {Id = 2, Name = "Hasan Cengiz", Email = "info@hasan.com", Photo= "25.jpg", Department = "Muhasebe"},
            new() {Id = 3, Name = "John Doe", Email = "info@johndoe.com", Photo= "58.jpg", Department = "İK"},
            new() {Id = 4, Name = "Peter Parker", Email = "info@pp.com", Photo= "63.jpg", Department = "Muhasebe"},
            new() {Id = 5, Name = "Tony Stark", Email = "info@tonystank.com", Photo= "93.jpg", Department = "Yazılım"},
        };
    }

    public IEnumerable<Employee> GetAll()
    {
        return _employeeList;
    }

    public Employee GetbyId(int id)
    {
        return _employeeList.FirstOrDefault(i => i.Id == id);
    }

    public Employee Update(Employee entity)
    {
        Employee employee = _employeeList.FirstOrDefault(i => i.Id == entity.Id);

        if (employee != null)
        {
            employee.Name = entity.Name;
            employee.Email = entity.Email;
            employee.Photo = entity.Photo;
            employee.Department = entity.Department;
        }
        return employee;
    }
}
