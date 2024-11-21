using TiendaOnline.Models;

namespace TiendaOnline.Factories
{
    public class ProductoFactory : IProductoFactory
    {
        public Producto CrearProducto(string nombre, string descripcion, decimal precio, string categoria, int cantidadInventario, string imageUrl, string imageUrlSecond)
        {
            switch (categoria.ToLower())
            {
                case "hombre":
                    return new ProductoHombre
                    {
                        Nombre = nombre,
                        Descripcion = descripcion,
                        Precio = precio,
                        Categoria = categoria,
                        CantidadInventario = cantidadInventario,
                        ImageUrl = imageUrl,
                        ImageUrlSecond = imageUrlSecond
                    };
                case "mujer":
                    return new ProductoMujer
                    {
                        Nombre = nombre,
                        Descripcion = descripcion,
                        Precio = precio,
                        Categoria = categoria,
                        CantidadInventario = cantidadInventario,
                        ImageUrl = imageUrl,
                        ImageUrlSecond = imageUrlSecond
                    };
                case "niño":
                    return new ProductoNino
                    {
                        Nombre = nombre,
                        Descripcion = descripcion,
                        Precio = precio,
                        Categoria = categoria,
                        CantidadInventario = cantidadInventario,
                        ImageUrl = imageUrl,
                        ImageUrlSecond = imageUrlSecond
                    };
                default:
                    return new Producto
                    {
                        Nombre = nombre,
                        Descripcion = descripcion,
                        Precio = precio,
                        Categoria = categoria,
                        CantidadInventario = cantidadInventario,
                        ImageUrl = imageUrl,
                        ImageUrlSecond = imageUrlSecond
                    };
            }
        }
    }

}
