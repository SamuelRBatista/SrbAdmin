using SrbComercialAdmin.Models;

public class StateService
{
    private readonly HttpClient _httpClient;

    public StateService(HttpClient httpClient)
    {
        _httpClient = httpClient; 
    }

    public async Task<List<State>> GetStatesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<State>>("https://localhost:7056/api/State");
    }
}
