using System.Text.Json.Serialization;

namespace SrbComercialAdmin.Models;
public class Product
{
    public int Id { get; set; }

    [JsonPropertyName("ean")]
    public string Ean { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("image_Url")]
    public string ImageUrl { get; set; }
}
