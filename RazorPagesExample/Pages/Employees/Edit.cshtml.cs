using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesExample.Models;
using RazorPagesExample.Repository.Abstract;

namespace RazorPagesExample.Pages.Employees;


public class EditModel : PageModel
{
    public Employee Employee { get; set; } = null!;
    private readonly IEmployeesRepository _employeeRepository;

    public EditModel(IEmployeesRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IActionResult OnGet(int id)
    {
        Employee = _employeeRepository.GetbyId(id);

        if (Employee == null)
        {
            return RedirectToPage("/NotFound");
        }
        return Page();
    }

    public IActionResult OnPost(Employee employee)
    {
        Employee = _employeeRepository.Update(employee);
        return RedirectToPage("/Employees/Index");
    }
}