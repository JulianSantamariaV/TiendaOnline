using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TiendaOnline.Factories;
using TiendaOnline.Models;
using TiendaOnline.Repositories;

namespace TiendaOnline.Controllers
{
    public class ProductoesController(IRepository<Producto> productoRepository, IProductoFactory productoFactory) : Controller
    {
        private readonly IRepository<Producto> _productoRepository = productoRepository;
        private readonly IProductoFactory _productoFactory = productoFactory;

        // GET: Productoes
        public async Task<IActionResult> Index()
        {
            return View(await _productoRepository.GetAllAsync());
        }

        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _productoRepository.GetByIdAsync(id.Value);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Creater([Bind("Nombre,Descripcion,Precio,Categoria,CantidadInventario,ImageUrl,ImageUrlSecond")] Producto productoInput)
        {
            if (ModelState.IsValid)
            {
                // Uso del factory para crear el producto
                var producto = _productoFactory.CrearProducto(
                    productoInput.Nombre,
                    productoInput.Descripcion,
                    productoInput.Precio,
                    productoInput.Categoria,
                    productoInput.CantidadInventario,
                    productoInput.ImageUrl,
                    productoInput.ImageUrlSecond
                );

                await _productoRepository.AddAsync(producto);
                return RedirectToAction(nameof(Index));
            }
            return View(productoInput);
        }      

        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _productoRepository.GetByIdAsync(id.Value);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoID,Nombre,Descripcion,Precio,Categoria,CantidadInventario,ImageUrl,ImageUrlSecond")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                await _productoRepository.AddAsync(producto);
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // POST: Productoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoID,Nombre,Descripcion,Precio,Categoria,CantidadInventario,ImageUrl,ImageUrlSecond")] Producto producto)
        {
            if (id != producto.ProductoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productoRepository.UpdateAsync(producto);
                }
                catch
                {
                    if (await _productoRepository.GetByIdAsync(producto.ProductoID) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(producto);
        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _productoRepository.GetByIdAsync(id.Value);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
