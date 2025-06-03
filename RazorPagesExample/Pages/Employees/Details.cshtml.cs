using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesExample.Models;
using RazorPagesExample.Repository.Abstract;

namespace RazorPagesExample.Pages.Employees;


public class DetailsModel : PageModel
{
    public Employee Employee { get; set; } = default!;
    private readonly IEmployeesRepository _employeeRepository;

    public DetailsModel(IEmployeesRepository employeeRepository)
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
}