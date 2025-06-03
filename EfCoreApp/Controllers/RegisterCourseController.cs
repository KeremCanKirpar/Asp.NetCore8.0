using EfCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EfCoreApp.Controllers;


public class RegisterCourseController : Controller
{
    private readonly DataContext _context;

    public RegisterCourseController(DataContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var registerCourse = await _context.RegisterCourses
            .Include(s => s.Student)
            .Include(s => s.Course)
            .ToListAsync();
        return View(registerCourse);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Students = new SelectList(await _context.Students.ToListAsync(), "StudentId", "NameSurname");
        ViewBag.Courses = new SelectList(await _context.Courses.ToListAsync(), "CourseId", "CourseName");
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RegisterCourse model)
    {
        _context.RegisterCourses.Add(model);
        model.RegisterDate = DateTime.Now;
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "RegisterCourse");
    }
}