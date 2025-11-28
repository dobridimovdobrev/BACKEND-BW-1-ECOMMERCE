using Microsoft.AspNetCore.Mvc;

namespace BW_1_S4_L1.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
