using Microsoft.AspNetCore.Mvc;
using TiendaOnline.Data;
using TiendaOnline.Services;

namespace TiendaOnline.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly GestionInventarioContext _context;

        public ShoppingCartController(GestionInventarioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = ShoppingCartSession.GetShoppingCart(HttpContext);
            return View(cart); // Asegúrate de pasar el carrito a la vista
        }

        public IActionResult Add(int productoId, int cantidad)
        {
            // Obtiene el producto por su ID
            var producto = _context.Productos.FirstOrDefault(p => p.ProductoID == productoId);

            // Verifica si el producto existe
            if (producto == null)
            {
                return NotFound(); // Producto no encontrado
            }

            // Obtiene el carrito de la sesión y añade el producto
            var carrito = ShoppingCartSession.GetShoppingCart(HttpContext);
            carrito.AddItem(producto, cantidad);

            // Guarda el carrito actualizado en la sesión
            HttpContext.Session.SetObject("Carrito", carrito);

            return RedirectToAction("Index");
        }
    }
}
