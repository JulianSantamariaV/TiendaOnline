using TiendaOnline.Models.ShoppingCart;

namespace TiendaOnline.Services
{
    public class ShoppingCartSession
    {
        public static ShoppingCart GetShoppingCart(HttpContext context)
        {
            var cart = (ShoppingCart)context.Session.GetObject<ShoppingCart>("Cart");
            if (cart == null)
            {
                cart = ShoppingCart.ObtainInstance();
                context.Session.SetObject("Cart", cart);
            }
            return cart;
        }
    }
}
