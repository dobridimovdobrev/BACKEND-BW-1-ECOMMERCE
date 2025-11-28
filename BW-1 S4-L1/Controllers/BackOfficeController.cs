using BW_1_S4_L1.Helpers;
using BW_1_S4_L1.Models;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND_BW_1_ECOMMERCE.Controllers
{
    public class BackOfficeController : Controller
    {
        public IActionResult Index()
        {
            var products = DbHelper.GetAllProducts();
            return View();
        }

        [HttpGet]
        public IActionResult Modify()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            DbHelper.AddProduct(product);
            return RedirectToAction("Index");
        }

    }
}

