using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace StoreApp.Web.Models;

public class OrderModel
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    [Display(Name = "İsim Soyisim")]
    [Required(ErrorMessage = "İsim Soyisim Boş olmamalıdır")]
    public string Name { get; set; } = null!;
    [Display(Name = "Şehir")]
    [Required(ErrorMessage = "Şehir Boş olmamalıdır")]
    public string City { get; set; } = null!;
    [Display(Name = "Telefon Numaranız")]
    [Required(ErrorMessage = "Telefon Numarası Boş olmamalıdır")]
    public string Phone { get; set; } = null!;
    [Display(Name = "E-Posta Adresiniz")]
    [Required(ErrorMessage = "E-Posta adresiniz Boş olmamalıdır")]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Display(Name = "Adres Bilgileriniz")]
    [Required(ErrorMessage = "Adres Bilgileriniz Boş olmamalıdır")]
    public string AddressLine { get; set; } = null!;
    [BindNever]
    public Cart Cart { get; set; } = null!;

    [Required(ErrorMessage = "Kart sahibi adı boş olamaz")]
    [Display(Name = "Kart Sahibi")]
    public string? CardName { get; set; }

    [Required(ErrorMessage = "Kart numarası boş olamaz")]
    [Display(Name = "Kart Numarası")]
    [CreditCard(ErrorMessage = "Geçersiz kart numarası")]
    public string? Cardnumber { get; set; }

    [Required(ErrorMessage = "Son kullanma ayı boş olamaz")]
    [Display(Name = "Son Kullanma Ayı")]
    [Range(1, 12, ErrorMessage = "Geçersiz ay")]
    public string? ExpirationMonth { get; set; }

    [Required(ErrorMessage = "Son kullanma yılı boş olamaz")]
    [Display(Name = "Son Kullanma Yılı")]
    [Range(2024, 2030, ErrorMessage = "Geçersiz yıl")]
    public string? ExpirationYear { get; set; }

    [Required(ErrorMessage = "CVC kodu boş olamaz")]
    [Display(Name = "CVC")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "CVC kodu 3 haneli olmalıdır")]
    public string? Cvc { get; set; }
}
