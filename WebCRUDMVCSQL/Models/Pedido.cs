using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCRUDMVCSQL.Models
{
    [Table("Pedido")]
    public class Pedido
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("ClienteId")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [Column("Data")]
        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        public ICollection<PedidoItem> Itens { get; set; }
    }
}
