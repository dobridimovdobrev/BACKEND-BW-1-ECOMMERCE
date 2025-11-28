using BW_1_S4_L1.Helpers;
using BW_1_S4_L1.Models;
using Microsoft.AspNetCore.Mvc;

namespace BW_1_S4_L1.Controllers
{
    public class BackOfficeController : Controller
    {
        public IActionResult Index()
        {
            var products = DbHelper.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product, int CategoryId)
        {
            int productId = DbHelper.AddProductAndGetId(product);
            DbHelper.AddProductToCategory(productId, CategoryId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Modify(int id)
        {
            var product = DbHelper.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Modify(Product product)
        {
            DbHelper.ModifyProduct(product);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            DbHelper.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}