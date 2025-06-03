using System;
using EfCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EfCoreApp.Controllers;


public class StudentController : Controller
{
    private readonly DataContext _context;

    public StudentController(DataContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Student");
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var students = await _context.Students.ToListAsync();
        return View(students);
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var student = await _context
        .Students
        .Include(s => s.RegisterCourses)
        .ThenInclude(s => s.Course)
        .FirstOrDefaultAsync(s => s.StudentId == id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Student student)
    {
        if (id != student.StudentId)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Students.Any(s => s.StudentId == student.StudentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Student");
        }
        return View(student);
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }
    [HttpPost]
    public async Task<IActionResult> Delete([FromForm]int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index", "Student");
    }
}