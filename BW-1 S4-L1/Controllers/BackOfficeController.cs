using BW_1_S4_L1.Helpers;
using BW_1_S4_L1.Models;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND_BW_1_ECOMMERCE.Controllers
{
    public class BackOfficeController : Controller
    {
        public IActionResult Index()
        {
            var products = DbHelpers.GetProducts();
            return View(products);
        }

        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Modify(int id)
        {
            var product = DbHelpers.getProductById(id);
            return View(product);
        }


        [HttpPost]
        public IActionResult Modify(Product product)
        {
            DbHelpers.ModifyProduct(product);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            DbHelpers.SaveProduct(product);

            return RedirectToAction("Index");
        }
        public IActionResult Delete()
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            DbHelpers.DeleteProduct(id);
            return RedirectToAction("Index");
        }







    }
}

