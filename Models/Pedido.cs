using System.ComponentModel.DataAnnotations;
using TiendaOnline.Models.Usuario;

namespace TiendaOnline.Models
{
    public enum EstadoPedido
    {
        Pendiente,
        EnProceso,
        Completado,
        Cancelado
    }

    public class Pedido
    {
        [Key]
        public int PedidoID { get; set; }

        public int ClienteID { get; set; }

        [Required]
        public DateTime FechaPedido { get; set; } = DateTime.Now;

        [Required]
        public EstadoPedido Estado { get; set; } 

        public decimal Total { get; set; }

        public Cliente Cliente { get; set; }

        public ICollection<DetallePedido> Detalles { get; set; }
    }
}
