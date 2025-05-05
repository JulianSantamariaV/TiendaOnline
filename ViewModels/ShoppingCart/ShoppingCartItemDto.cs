namespace TiendaOnline.ViewModels.ShoppingCart
{
    public class ShoppingCartItemDto
    {
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string ImageUrl { get; set; }
    }
}
