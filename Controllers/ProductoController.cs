using Microsoft.AspNetCore.Mvc;
using TiendaOnline.Models;
using TiendaOnline.Repositories;

namespace TiendaOnline.Controllers
{
    public class ProductoController(IRepository<Producto> productoRepository) : Controller
    {
        private readonly IRepository<Producto> _productoRepository = productoRepository;

        public async Task<IActionResult> Index()
        {
            var productos = await _productoRepository.GetAllAsync();
            return View(productos);
        }

        public async Task<IActionResult> Hombre()
        {
            var productosHombre = (await _productoRepository.GetAllAsync())
                .Where(p => p.Categoria?.ToLower() == "hombre").ToList();

            if (productosHombre.Count == 0)
            {
                ViewBag.Message = "No se encontraron productos en la categoría 'hombre'.";
            }

            return View(productosHombre);
        }

        public async Task<IActionResult> Mujer()
        {
            var productosMujer = (await _productoRepository.GetAllAsync())
                .Where(p => !string.IsNullOrEmpty(p.Categoria) && p.Categoria.Equals("mujer", StringComparison.CurrentCultureIgnoreCase)).ToList();

            if (productosMujer.Count == 0)
            {
                ViewBag.Message = "No se encontraron productos en la categoría 'mujer'.";
            }

            return View(productosMujer);
        }

        public async Task<IActionResult> Niño()
        {
            var productosNiño = (await _productoRepository.GetAllAsync())
                .Where(p => p.Categoria?.ToLower() == "niño").ToList();

            if (productosNiño.Count == 0)
            {
                ViewBag.Message = "No se encontraron productos en la categoría 'niño'.";
            }

            return View(productosNiño);
        }


    }
}
