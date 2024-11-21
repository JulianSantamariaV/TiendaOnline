using TiendaOnline.Models;

namespace TiendaOnline.Factories
{
    public class ProductoMujer : Producto
    {
        public void MostrarDetalles()
        {
           
            Console.WriteLine($"Producto de Mujer: {Nombre}, Precio: {Precio}");
        }
    }
}
