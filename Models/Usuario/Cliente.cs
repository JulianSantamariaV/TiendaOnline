namespace TiendaOnline.Models.Usuario
{
    public class Cliente : Usuario
    {
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        // Relación con los pedidos
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }

}
