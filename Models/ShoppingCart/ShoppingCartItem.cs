using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UsuarioModel = TiendaOnline.Models.Usuario.Usuario;


namespace TiendaOnline.Models.ShoppingCart
{    
    public class ShoppingCartItem
    {
        [Key]
        public int ShoppingCartItemID { get; set; }

        [Required]
        public int ProductoID { get; set; }

        [ForeignKey("ProductoID")]
        public Producto Producto { get; set; } = null!;

        [Required]
        public int Cantidad { get; set; }

        // Relación con Usuario (por ID)
        public int? UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public UsuarioModel? Usuario { get; set; }

    }
}
