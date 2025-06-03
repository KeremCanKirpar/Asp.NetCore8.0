using System.ComponentModel.DataAnnotations;

namespace EfCoreApp.Data;

public class Teacher
{
    public int TeacherId { get; set; }

    [Display(Name = "Öğretmen Adı")]
    public string? Name { get; set; }

    [Display(Name = "Öğretmen Soyadı")]
    public string? Surname { get; set; }

    [Display(Name = "Öğretmen Adı Soyadı")]
    public string NameSurname
    {
        get
        {
            return $"{Name} {Surname}";
        }
    }

    [Display(Name = "Telefon Numarası")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "E-posta Adresi")]
    public string? Email { get; set; }

    public ICollection<Course> Courses { get; set; } = new List<Course>();
}