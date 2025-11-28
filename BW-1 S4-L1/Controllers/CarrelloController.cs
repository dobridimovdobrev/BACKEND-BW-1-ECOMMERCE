using BW_1_S4_L1.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BW_1_S4_L1.Controllers
{
    public class CarrelloController : Controller
    {
        public IActionResult Index()
        {
            var cartItems = DbHelper.GetCartItemsWithProducts();
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult Add(int productId, int quantity, string? size)
        {
            DbHelper.AddToCart(productId, quantity, size);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            DbHelper.RemoveFromCart(id);
            return RedirectToAction("Index");
        }
    }
}
