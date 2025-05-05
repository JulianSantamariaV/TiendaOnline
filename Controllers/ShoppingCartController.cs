using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaOnline.Data;
using TiendaOnline.Models.ShoppingCart;
using TiendaOnline.Services;
using TiendaOnline.ViewModels.ShoppingCart;

namespace TiendaOnline.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly GestionInventarioContext _context;
        private readonly ShoppingCartService _shoppingCartService;

        // Constructor para inyectar dependencias
        public ShoppingCartController(GestionInventarioContext context, ShoppingCartService shoppingCartService)
        {
            _context = context;
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(int productoId, int cantidad)
        {
            var producto = _context.Productos.Find(productoId);
            if (producto == null)
            {
                return NotFound();
            }

            if (User.Identity?.IsAuthenticated == true)
            {
                int usuarioId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
                var item = _context.ShoppingCartItems
                    .FirstOrDefault(c => c.UsuarioId == usuarioId && c.ProductoID == productoId);

                if (item != null)
                {
                    item.Cantidad += cantidad;
                }
                else
                {
                    _context.ShoppingCartItems.Add(new ShoppingCartItem
                    {
                        ProductoID = productoId,
                        Cantidad = cantidad,
                        UsuarioId = usuarioId
                    });
                }
                _context.SaveChanges();
            }
            else
            {
                // Usuario no autenticado → usar sesión
                var cartItems = _shoppingCartService.GetCartItemsFromSession(HttpContext.Session); // Usamos el servicio inyectado

                var existing = cartItems.FirstOrDefault(c => c.ProductoID == productoId);
                if (existing != null)
                {
                    existing.Cantidad += cantidad;
                }
                else
                {
                    cartItems.Add(new ShoppingCartItemDto
                    {
                        ProductoID = productoId,
                        Cantidad = cantidad
                    });
                }

                // Convertir a ShoppingCartItem antes de guardar en sesión
                var shoppingCartItems = cartItems.Select(item => new ShoppingCartItem
                {
                    ProductoID = item.ProductoID,
                    Cantidad = item.Cantidad
                }).ToList();

                _shoppingCartService.SaveCartToSession(HttpContext.Session, shoppingCartItems); // Usamos el servicio inyectado
            }

            return RedirectToAction("Cart");
        }

        public IActionResult Cart()
        {
            List<ShoppingCartItemDto> items;

            if (User.Identity?.IsAuthenticated == true)
            {
                int usuarioId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
                items = _shoppingCartService.GetCartItemsFromDatabase(usuarioId); // Usamos el servicio inyectado
            }
            else
            {
                items = _shoppingCartService.GetCartItemsFromSession(HttpContext.Session); // Usamos el servicio inyectado
            }

            return View(items ?? new List<ShoppingCartItemDto>());
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productoId)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                int usuarioId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
                var item = _context.ShoppingCartItems
                    .FirstOrDefault(c => c.UsuarioId == usuarioId && c.ProductoID == productoId);

                if (item != null)
                {
                    _context.ShoppingCartItems.Remove(item);
                    _context.SaveChanges();
                }
            }
            else
            {
                var cartItems = _shoppingCartService.GetCartItemsFromSession(HttpContext.Session);
                var item = cartItems.FirstOrDefault(c => c.ProductoID == productoId);

                if (item != null)
                {
                    cartItems.Remove(item);

                    // Convertir los DTOs a ShoppingCartItem antes de guardar en sesión
                    var shoppingCartItems = cartItems.Select(cartItem => new ShoppingCartItem
                    {
                        ProductoID = cartItem.ProductoID,
                        Cantidad = cartItem.Cantidad
                    }).ToList();

                    _shoppingCartService.SaveCartToSession(HttpContext.Session, shoppingCartItems); // Usamos el servicio inyectado
                }
            }
            return RedirectToAction("Cart");
        }
    }
}
