using System.ComponentModel.DataAnnotations;

namespace IdentityApp.ViewModels;


public class CreateViewModel
{
    [Required]
    [Display(Name ="Kullanıcı Adı ")]
    public string? UserName { get; set; }
    [Required]
    [Display(Name = "Ad Soyad ")]
    public string? FullName { get; set; }
    [Required]
    [Display(Name = " E-Posta ")]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string? Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage ="Şifreler Uyuşmuyor")]
    [Display(Name = "Şifreyi Tekrarla ")]
    public string? ConfirmPassword { get; set; }

}

