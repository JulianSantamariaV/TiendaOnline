using TiendaOnline.Models.ShoppingCart;

namespace TiendaOnline.Models.Usuario
{
    public class Usuario
    {
        
        public int UsuarioId { get; set; }
        
        public  string Nombre { get; set; }
  
        public  string Email { get; set; }
     
        public  string PasswordHash { get; set; }
        
        public int TipoUsuario { get; set; }

        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

    }
}
