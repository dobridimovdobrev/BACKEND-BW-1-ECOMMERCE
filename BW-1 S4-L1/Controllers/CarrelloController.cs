using Microsoft.AspNetCore.Mvc;

namespace BACKEND_BW_1_ECOMMERCE.Controllers
{
    public class CarrelloController : Controller
    {
        public IActionResult Carrello()
        {
            return View();
        }
    }
}
