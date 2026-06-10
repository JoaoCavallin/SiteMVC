using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCRUDMVCSQL.ViewModels
{
    public class ProdutoViewModel
    {

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Peso é obrigatório")]
        [Range(0.1, 700.0, ErrorMessage = "Peso deve estar entre 0,1 e 700 kg")]
        public decimal Peso { get; set; }

        [Required(ErrorMessage = "Preço é obrigatório")]
        [Range(0.1, 100000.0, ErrorMessage = "Preço deve estar entre 0,1 e 100000 dinheiros")]
        public decimal Preco { get; set; }

    }
}
