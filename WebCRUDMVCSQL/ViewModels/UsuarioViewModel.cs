using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCRUDMVCSQL.ViewModels
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Entre 3 e 50 caracteres")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Nome não pode conter números")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [StringLength(50, ErrorMessage = "Email muito longo")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Entre 5 e 20 caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}