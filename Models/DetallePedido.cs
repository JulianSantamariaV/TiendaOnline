using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Models
{
    public class DetallePedido
    {
        [Key]
        public int DetalleID { get; set; }  
        public int PedidoID { get; set; }
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        // Relaciones con otras entidades
        public Pedido Pedido { get; set; }
        public Producto Producto { get; set; }
    }

}
