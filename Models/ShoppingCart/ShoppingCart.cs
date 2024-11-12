namespace TiendaOnline.Models.ShoppingCart
{
    public class ShoppingCart
    {
        private static ShoppingCart _shoppingCart;
        private static readonly object _lock = new object();

        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        private ShoppingCart() { }

        public static ShoppingCart ObtainInstance()
        {
            if (_shoppingCart == null)
            {
                lock (_lock)
                {
                    if (_shoppingCart == null)
                    {
                        _shoppingCart = new ShoppingCart();
                    }
                }
            }
            return _shoppingCart;
        }

        public void AddItem(Producto producto, int cantidad)
        {
            var item = Items.FirstOrDefault(i => i.Producto.ProductoID == producto.ProductoID);
            if (item == null)
            {
                Items.Add(new ShoppingCartItem { Producto = producto, Cantidad = cantidad });
            }
            else
            {
                item.Cantidad += cantidad;
            }
        }

        public void DropItem(int productoId)
        {
            var item = Items.FirstOrDefault(i => i.Producto.ProductoID == productoId);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        public decimal TotalAmount()
        {
            return Items.Sum(i => i.Producto.Precio * i.Cantidad);
        }
    }
}

