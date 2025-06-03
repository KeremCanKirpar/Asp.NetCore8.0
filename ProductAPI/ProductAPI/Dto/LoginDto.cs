using System;
using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Dto;

public class LoginDto
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
