using BW_1_S4_L1.Helpers;
using BW_1_S4_L1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BACKEND_BW_1_ECOMMERCE.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            var products = DbHelper.GetAllProducts();
            return View();
        }

    }
}
