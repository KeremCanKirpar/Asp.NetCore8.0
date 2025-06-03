using System.Threading.Tasks;
using EfCoreApp.Data;
using EfCoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EfCoreApp.Controllers;


public class CourseController : Controller
{
    private readonly DataContext _context;

    public CourseController(DataContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var courses = await _context
        .Courses
        .Include(c => c.Teacher)
        .ToListAsync();
        return View(courses);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Teachers = new SelectList(await _context.Teachers.ToListAsync(), "TeacherId", "NameSurname");
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CourseViewModel model)
    {
        if (ModelState.IsValid)
        {
            _context.Courses.Add(new Course()
            {
                CourseId = model.CourseId,
                CourseName = model.CourseName,
                TeacherId = model.TeacherId
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Course");
        }
        ViewBag.Teachers = new SelectList(await _context.Teachers.ToListAsync(), "TeacherId", "NameSurname");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var courseEntity = await _context
            .Courses
            .Include(c => c.RegisterCourses!)
                .ThenInclude(rc => rc.Student)
            .FirstOrDefaultAsync(c => c.CourseId == id);

        if (courseEntity == null)
        {
            return NotFound();
        }

        var course = new CourseViewModel()
        {
            CourseId = courseEntity.CourseId,
            CourseName = courseEntity.CourseName,
            TeacherId = courseEntity.TeacherId,
            RegisterCourses = courseEntity.RegisterCourses
        };

        ViewBag.Teachers = new SelectList(await _context.Teachers.ToListAsync(), "TeacherId", "NameSurname");
        return View(course);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CourseViewModel model)
    {
        if (id != model.CourseId)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(new Course()
                {
                    CourseId = model.CourseId,
                    CourseName = model.CourseName,
                    TeacherId = model.TeacherId
                });
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Courses.Any(c => c.CourseId == model.CourseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Course");
        }
        ViewBag.Teachers = new SelectList(await _context.Teachers.ToListAsync(), "TeacherId", "NameSurname");
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var course = await _context.Courses.FirstOrDefaultAsync(s => s.CourseId == id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index", "Course");
    }
}
