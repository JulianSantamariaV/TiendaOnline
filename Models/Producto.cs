using Humanizer;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TiendaOnline.Models
{
    public class Producto
    {
        [Key]
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
        public int CantidadInventario { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrlSecond { get; set; }
    }
}
