using SrbComercialAdmin.Models;

namespace SrbComercialAdmin.Services;

public class CityService
{
    private readonly HttpClient _httpClient;

    public CityService(HttpClient httpClient)
    {
        _httpClient = httpClient; 
    }

    public async Task<List<City>> GetCitiesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<City>>("https://localhost:7056/api/City");
    }

    public async Task<List<City>> GetCitiesByStateIdAsync(int stateId)
    {             
       return await _httpClient.GetFromJsonAsync<List<City>>("https://localhost:7056/api/City/ByState/{stateId}");
    }

    public async Task<City?> GetCityByIdAsync(int cityId)
    {
        return await _httpClient.GetFromJsonAsync<City>($"https://localhost:7056/api/City/{cityId}");
    }
}
