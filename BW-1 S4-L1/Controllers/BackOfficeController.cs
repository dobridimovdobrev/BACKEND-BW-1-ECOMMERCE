using Microsoft.AspNetCore.Mvc;

namespace BACKEND_BW_1_ECOMMERCE.Controllers
{
    public class BackOfficeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        public IActionResult Modify()
        {
            return View();
        }


    }
}

