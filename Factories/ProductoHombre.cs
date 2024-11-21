using TiendaOnline.Models;

namespace TiendaOnline.Factories
{
    public class ProductoHombre : Producto
    {
        public void MostrarDetalles()
        {          
            Console.WriteLine($"Producto de Hombre: {Nombre}, Precio: {Precio}");
        }
    }
}
