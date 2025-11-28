using BW_1_S4_L1.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND_BW_1_ECOMMERCE.Controllers
{
    public class CarrelloController : Controller
    {
        public IActionResult Index()
        {
            var cartItems = DbHelper.GetCartItems();
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult Add(int productId, int quantity, string? size)
        {
            DbHelper.AddToCart(productId, quantity, size);
            return RedirectToAction("Index", "Home");
        }
    }
}
