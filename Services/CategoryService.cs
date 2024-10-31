using SrbComercialAdmin.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


public class CategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7056/api/Category");
    }
}
