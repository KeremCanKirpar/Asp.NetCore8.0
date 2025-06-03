using System.ComponentModel.DataAnnotations;

namespace IdentityApp.ViewModels;

public class EditViewModel
{
    public string? Id { get; set; }

    [Required]
    [Display(Name = "Kullanıcı Adı")]
    public string? UserName { get; set; }

    [Required]
    [Display(Name = "Ad Soyad")]
    public string? FullName { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "E-Posta")]
    public string? Email { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string? Password { get; set; }

    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Şifreler Uyuşmuyor")]
    [Display(Name = "Şifreyi Tekrarla ")]
    public string? ConfirmPassword { get; set; }

    public IList<string>? SelectedRoles { get; set; }
}

