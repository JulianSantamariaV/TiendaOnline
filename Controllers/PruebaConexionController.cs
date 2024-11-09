using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TiendaOnline.Data;

namespace TiendaOnline.Controllers
{
    public class PruebaConexionController : Controller
    {
        private readonly GestionInventarioContext _context;

        public PruebaConexionController(GestionInventarioContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Intenta obtener el primer producto en la tabla Productos
                var producto = await _context.Productos.FirstOrDefaultAsync();

                // Verifica si se obtuvieron datos
                if (producto != null)
                {
                    return Content("Conexión exitosa. Producto encontrado: " + producto.Nombre);
                }
                else
                {
                    return Content("Conexión exitosa, pero no se encontraron productos.");
                }
            }
            catch (Exception ex)
            {
                return Content("Error al conectarse a la base de datos: " + ex.Message);
            }
        }
    }
}
//copiar para boton de prueba < a class= "btn btn-primary" asp - action = "Index" asp - controller = "PruebaConexion" >PRUEBA</ a >
