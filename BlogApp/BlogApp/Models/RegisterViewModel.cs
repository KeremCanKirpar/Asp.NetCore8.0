using System;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Kullanıcı Adı")]
    public string? UserName { get; set; }
    [Required]
    [Display(Name = "Ad Soyad ")]
    public string? Name { get; set; }
  
    [Required]
    [EmailAddress]
    [Display(Name = "E-Posta")]
    public string? Email { get; set; }
    [Required]
    [StringLength(10, ErrorMessage = "{0} alanı en az {2} karakter uzunluğunda olmalıdır"), MinLength(6)]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string? Password { get; set; }
    [Required]
    [StringLength(10, ErrorMessage = "{0} alanı en az {2} karakter uzunluğunda olmalıdır"), MinLength(6)]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre Tekrar")]
    [Compare(nameof(Password),ErrorMessage ="Şifreler Uyuşmuyor")]
    public string? ConfirmPassword { get; set; }
}
