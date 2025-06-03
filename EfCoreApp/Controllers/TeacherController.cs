using EfCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EfCoreApp.Controllers;


public class TeacherController : Controller
{
    private readonly DataContext _context;

    public TeacherController(DataContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Teacher teacher)
    {
        await _context.Teachers.AddAsync(teacher);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Teacher");
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var teachers = await _context.Teachers.ToListAsync();
        return View(teachers);
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }
        return View(teacher);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Teacher model)
    {
        if (id != model.TeacherId)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Teachers.Any(t => t.TeacherId == model.TeacherId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return RedirectToAction("Index", "Teacher");
        }
        return View(model);
    }
}