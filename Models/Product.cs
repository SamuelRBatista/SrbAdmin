using System.ComponentModel.DataAnnotations;

namespace SrbComercialAdmin.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Ean { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser positivo")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "A quantidade deve ser positiva")]
        public int Quantity { get; set; }

        [Url(ErrorMessage = "A URL da imagem deve ser válida")]
        public string Image_Url { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public int CategoryId { get; set; }  // Este campo armazenará o ID da categoria selecionada

        // Opção para carregar categorias na view
        // Para preencher a lista de categorias no formulário
    }
    
}
