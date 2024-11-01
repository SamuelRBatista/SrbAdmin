using SrbComercialAdmin.Models;

namespace SrbComercialAdmin.Services;

public class ClientService
{ 
    private readonly HttpClient _httpClient;
    public ClientService(HttpClient httpClient)
    {
            _httpClient = httpClient; 
    }

    public async Task<List<Client>> GetClientAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Client>>("https://localhost:7056/api/Client");
    }

    public async Task CreateClientAsync(Client client)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7056/api/Client", client);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateClientAsync(Client client)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://localhost:7056/api/Client/{client.Id}", client);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteClientAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"https://localhost:7056/api/Client/{id}");
        response.EnsureSuccessStatusCode();
    }
}
