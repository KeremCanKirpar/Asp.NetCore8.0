using System;
using System.Text.Json.Serialization;

namespace aspClientApp.Models;

public class ProductDto
{
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }
    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = null!;
    [JsonPropertyName("Price")]
    public decimal Price { get; set; }
}
