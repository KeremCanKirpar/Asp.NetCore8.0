using System.ComponentModel.DataAnnotations;
using EfCoreApp.Data;


namespace EfCoreApp.Models;

public class CourseViewModel
{
    public int CourseId { get; set; }

    [Display(Name = "Kurs Adı")]
    [Required(ErrorMessage = "Kurs adı boş olamaz.")]
    [StringLength(50, ErrorMessage = "Kurs adı en fazla 50 karakter olmalıdır.")]
    public string? CourseName { get; set; }

    public int TeacherId { get; set; }

    public ICollection<RegisterCourse>? RegisterCourses { get; set; } = new List<RegisterCourse>();

}

