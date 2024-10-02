using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lamazon.Web.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        public IActionResult ShoppingCart()
        {
            var shoppingCart = GetShoppingCart();
            if (!shoppingCart.ShoppingCartItems.Any())
            {
                return RedirectToAction("Index", "Products");
            }

            return View();
        }
    }
}
