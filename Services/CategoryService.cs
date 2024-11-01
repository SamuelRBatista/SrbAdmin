using SrbComercialAdmin.Models;
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
