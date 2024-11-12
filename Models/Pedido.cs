using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoID { get; set; }
        public int ClienteID { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Estado { get; set; }
        public decimal? Total { get; set; }

        // Relación con cliente
        public Cliente Cliente { get; set; }

        // Relación con DetallePedido
        public ICollection<DetallePedido> Detalles { get; set; }
    }

}
