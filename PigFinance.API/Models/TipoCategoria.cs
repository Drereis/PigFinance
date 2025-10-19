using System.ComponentModel.DataAnnotations;

namespace PigFinance.API.Models
{
    public enum TipoCategoria
    {
        [Display(Name = "Alimentação")]
        Alimentacao = 1,

        [Display(Name = "Transporte")]
        Transporte = 2,

        [Display(Name = "Moradia")]
        Moradia = 3,

        [Display(Name = "Lazer")]
        Lazer = 4,

        [Display(Name = "Outros")]
        Outros = 5
    }
}
