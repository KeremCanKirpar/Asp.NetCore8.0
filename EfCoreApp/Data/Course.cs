using System.ComponentModel.DataAnnotations;

namespace EfCoreApp.Data
{
    public class Course
    {
        public int CourseId { get; set; }

        [Display(Name = "Kurs Adı")]
        public string? CourseName { get; set; }
        
        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }
      
        public ICollection<RegisterCourse>? RegisterCourses { get; set; } = new List<RegisterCourse>();
    }
}