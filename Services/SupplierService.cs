using SrbComercialAdmin.Models;

namespace SrbComercialAdmin.Services;


public class SupplierService
{

    private readonly HttpClient _httpClient;

    public SupplierService(HttpClient httpClient)
    {
            _httpClient = httpClient;
    }

    public async Task<List<Supplier>> GetSupplierAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Supplier>>("https://localhost:7056/api/Supplier");
    }

    public async Task CreateSupplierAsync(Supplier supplier)
    {
        var response = await _httpClient.PostAsJsonAsync($"https://localhost:7056/api/Supplier", supplier);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateSupplierAsync(Supplier supplier)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://localhost:7056/api/Supplier/{supplier.Id}", supplier);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteSupplierAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"https://localhost:7056/api/Supplier/{id}");
        response.EnsureSuccessStatusCode();
    }
}
