using System.ComponentModel.DataAnnotations;

namespace PigFinance.API.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(100, ErrorMessage = "A descrição não pode exceder 100 caracteres.")]
        public string Descricao { get; set; } 

        [Required(ErrorMessage = "O valor é obrigatório.")]
        [Range(-10000000.00, 10000000.00, ErrorMessage = "O valor da transação deve estar entre R$-10M e R$10M.")]
        public decimal Valor { get; set; } 

        [Required(ErrorMessage = "A data é obrigatória.")]
        public DateTime Data { get; set; } 

 
        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public TipoCategoria Categoria { get; set; } 



        [Required(ErrorMessage = "O proprietário da transação é obrigatório.")]
        public int UsuarioId { get; set; } 
        public Usuario? Usuario { get; set; } 
    }
}
