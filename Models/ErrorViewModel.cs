namespace SrbComercialAdmin.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; } // Usando Nullable Reference Type

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId); // Propriedade que verifica se RequestId não é nulo ou vazio
    }
}
