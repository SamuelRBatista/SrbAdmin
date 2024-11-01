namespace SrbComercialAdmin.Models;

public class Supplier
{
    public int Id { get; set; }
    public string? Cnpj { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Cep { get; set; }
    public string? Address { get; set; }
    public int CityId { get; set; } 

    public int StateId { get; set; }    
   
}
