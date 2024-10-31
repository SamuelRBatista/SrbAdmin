using SrbComercialAdmin.Models;

namespace SrbComercialAdmin.Services;

public class ProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Product>> GetProductAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Product>>("https://localhost:7056/api/Product");
    }

    public async Task CreateProductAsync(Product product)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7056/api/Product", product);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateProductAsync(Product product)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://localhost:7056/api/Product/{product.Id}", product);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteProductAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"https://localhost:7056/api/Product/{id}");
        response.EnsureSuccessStatusCode();
    }





}
