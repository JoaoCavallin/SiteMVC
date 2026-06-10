using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCRUDMVCSQL.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Column("Id")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("Nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Column("Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Column("Senha")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}