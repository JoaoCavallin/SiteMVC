using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCRUDMVCSQL.Models
{
    [Table("Clientes")]
    public class Cliente
    {
            [Column("Id")]
            [Display(Name = "Código")]
            public int Id { get; set; }

            [Column("Nome")]
            [Display(Name = "Nome")]
            public string Nome { get; set; }

            [Column("Sexo")]
            [Display(Name = "Sexo")]
            public bool Sexo { get; set; }

            [Column("Email")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Column("CPF")]
            [Display(Name = "CPF")]
            public string CPF { get; set; }

             public string SexoTexto => Sexo ? "s" : "n";
    }
}
