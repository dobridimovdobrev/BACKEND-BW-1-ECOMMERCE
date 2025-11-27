using System.Diagnostics;
using BW_1_S4_L1.Models;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND_BW_1_ECOMMERCE.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}
