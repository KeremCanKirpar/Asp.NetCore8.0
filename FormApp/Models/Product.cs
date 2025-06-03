using System.ComponentModel.DataAnnotations;

namespace FormApp.Models;

public class Product
{
    [Display(Name = "Ürün ID")]
    public int Id { get; set; }
    [Display(Name = "Ürün İsmi")]
    [Required(ErrorMessage = "Ürün ismi boş olamaz.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ürün fiyatı boş olamaz.")]
    [Display(Name = "Ürün Fiyatı")]
    public decimal Price { get; set; }

    [Display(Name = "Ürün Resmi")]
    public string Image { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    [Display(Name = "Kategori")]
    public int CategoryId { get; set; }
}
