using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        // Relación con pedidos
        public ICollection<Pedido> Pedidos { get; set; }
    }

}
