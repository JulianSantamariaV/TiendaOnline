using TiendaOnline.Models;

namespace TiendaOnline.Factories
{
    public interface IProductoFactory
    {
        Producto CrearProducto(string nombre, string descripcion, decimal precio, string categoria, int cantidadInventario, string imageUrl, string imageUrlSecond);
    }

}
