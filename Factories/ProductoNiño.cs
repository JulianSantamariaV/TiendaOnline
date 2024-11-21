using TiendaOnline.Models;

namespace TiendaOnline.Factories
{
    public class ProductoNino : Producto
    {
        public void MostrarDetalles()
        {            
            Console.WriteLine($"Producto de Niño: {Nombre}, Precio: {Precio}");
        }
    }
}
