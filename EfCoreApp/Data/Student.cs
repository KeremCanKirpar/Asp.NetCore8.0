using System.ComponentModel.DataAnnotations;

namespace EfCoreApp.Data
{
    public class Student
    {
        public int StudentId { get; set; }
        [Display(Name = "Öğrenci Adı")]
        public string? StudentName { get; set; }
        [Display(Name = "Öğrenci Soyadı")]
        public string? StudentSurname { get; set; }
        public string NameSurname
        {
            get
            {
                return $"{StudentName} {StudentSurname}";
            }
        }
        [Display(Name = "Öğrenci Mail Adresi")]
        public string? Email { get; set; }
        [Display(Name = "Öğrenci Telefon Numarası")]
        public string? Phone { get; set; }
        
        public ICollection<RegisterCourse> RegisterCourses { get; set; } = new List<RegisterCourse>();
    }
}