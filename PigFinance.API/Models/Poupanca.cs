using System.ComponentModel.DataAnnotations;

namespace PigFinance.API.Models
{
    public class Poupanca
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da poupança é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O valor alvo é obrigatório.")]
        [Range(0.01, 10000000.00, ErrorMessage = "O valor alvo deve ser positivo.")]
        public decimal ValorAlvo { get; set; }

        public decimal ValorAtual { get; set; } = 0.00m; 

        
        public DateTime DataAlvo { get; set; }
    }
}
