using Microsoft.AspNetCore.Mvc;
using TiendaOnline.Data;
using TiendaOnline.Models;

namespace TiendaOnline.Controllers
{
    public class ProductoController : Controller
    {
        private readonly GestionInventarioContext _context;

        public ProductoController(GestionInventarioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var productos = _context.Productos.ToList();
            return View(productos);
        }

        public IActionResult Hombre()
        {
            var productosHombre = _context.Productos.Where(p => p.Categoria.ToLower() == "hombre").ToList();

            if (!productosHombre.Any())
            {
                ViewBag.Message = "No se encontraron productos en la categoría 'mujer'.";
            }

            return View(productosHombre);
        }

        public IActionResult Mujer()
        {
            var productosMujer = _context.Productos
                .Where(p => !string.IsNullOrEmpty(p.Categoria) && p.Categoria.ToLower() == "mujer")
                .ToList();

            if (!productosMujer.Any())
            {
                ViewBag.Message = "No se encontraron productos en la categoría 'mujer'.";
            }

            return View(productosMujer);
        }

        public IActionResult Niño()
        {
            var productosNiño = _context.Productos.Where(p => p.Categoria.ToLower() == "niño").ToList();

            if (!productosNiño.Any())
            {
                ViewBag.Message = "No se encontraron productos en la categoría 'mujer'.";
            }

            return View(productosNiño);
        }
    }
}
