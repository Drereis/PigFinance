using System.ComponentModel.DataAnnotations;

namespace PigFinance.API.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int TipoCategoria { get; set; }
        public int TipoPagamento { get; set; }
        public int TipoTransacao { get; set; }
        public int UsuarioId { get; set; }
        public int PoupancaId { get; set; }
        public DateTime Data {get; set;} 
    }
}