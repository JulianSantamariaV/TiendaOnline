using System.Text.Json;
using TiendaOnline.Data;
using TiendaOnline.Models.ShoppingCart;
using TiendaOnline.ViewModels.ShoppingCart;

namespace TiendaOnline.Services
{
    public class ShoppingCartService
    {
        private readonly GestionInventarioContext _context;
        private const string SessionKey = "CartItems";

        // Constructor para la inyección de dependencias
        public ShoppingCartService(GestionInventarioContext context)
        {
            _context = context;
        }

        // Obtiene los items del carrito desde la sesión, usando el DTO
        public List<ShoppingCartItemDto> GetCartItemsFromSession(ISession session)
        {
            var json = session.GetString(SessionKey);
            if (string.IsNullOrEmpty(json))
            {
                return new List<ShoppingCartItemDto>();
            }

            // Deserializamos a ShoppingCartItem (sin las propiedades de navegación), y luego convertimos a DTO
            var cartItems = JsonSerializer.Deserialize<List<ShoppingCartItem>>(json) ?? new List<ShoppingCartItem>();

            return cartItems.Select(item => new ShoppingCartItemDto
            {
                ProductoID = item.ProductoID,
                Cantidad = item.Cantidad,
                Nombre = item.Producto?.Nombre,  // Usar el producto si está presente
                Precio = item.Producto?.Precio ?? 0m     // Precio del producto, con valor por defecto si no existe
            }).ToList();
        }

        // Obtiene los items del carrito desde la base de datos
        public List<ShoppingCartItemDto> GetCartItemsFromDatabase(int usuarioId)
        {
            return _context.ShoppingCartItems
                .Where(c => c.UsuarioId == usuarioId)
                .Select(c => new ShoppingCartItemDto
                {
                    ProductoID = c.ProductoID,
                    Cantidad = c.Cantidad,
                    Nombre = c.Producto.Nombre,
                    Precio = c.Producto.Precio,
                    ImageUrl = c.Producto.ImageUrl
                })
                .ToList();
        }

        // Guarda el carrito en la sesión, usando ShoppingCartItem (sin propiedades de navegación)
        public void SaveCartToSession(ISession session, List<ShoppingCartItem> cartItems)
        {
            var json = JsonSerializer.Serialize(cartItems);
            session.SetString(SessionKey, json);
        }

        // Limpia el carrito de la sesión
        public void ClearCart(ISession session)
        {
            session.Remove(SessionKey);
        }
    }
}
