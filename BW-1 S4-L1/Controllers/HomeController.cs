using BW_1_S4_L1.Helpers;
using BW_1_S4_L1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BW_1_S4_L1.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index(int? categoryId)
        {
            List<Product> products;

            if (categoryId.HasValue)
            {
                products = DbHelper.GetProductsByCategory(categoryId.Value);
            }
            else
            {
                products = DbHelper.GetAllProducts();
            }

            return View(products);
        }

    }
}
