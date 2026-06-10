using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCRUDMVCSQL.ViewModels
{
    public class ClienteViewModel
    {
            [Required(ErrorMessage = "Nome é obrigatório")]
            [StringLength(100, MinimumLength = 3, ErrorMessage = "Entre 3 e 100 caracteres")]
            [RegularExpression(@"^[^\d]+$", ErrorMessage = "Nome não pode conter números")]
            public string Nome { get; set; }

            [Required(ErrorMessage = "Sexo é obrigatório")]
            public bool Sexo { get; set; }

            [Required(ErrorMessage = "Email é obrigatório")]
            [StringLength(50, ErrorMessage = "Email muito longo")]
            [EmailAddress(ErrorMessage = "Email inválido")]
            public string Email { get; set; }

            [Required(ErrorMessage = "CPF é obrigatório")]
            [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$",
            ErrorMessage = "CPF inválido. Use o formato 000.000.000-00")]
            public string CPF { get; set; }
    }
}
