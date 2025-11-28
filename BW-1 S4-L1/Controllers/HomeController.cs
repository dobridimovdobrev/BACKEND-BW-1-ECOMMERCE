using BW_1_S4_L1.Helpers;
using BW_1_S4_L1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BW_1_S4_L1.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index(int? categoryId, int page = 1)
        {
            int pageSize = 20;
            List<Product> allProducts;

            if (categoryId.HasValue)
            {
                allProducts = DbHelper.GetProductsByCategory(categoryId.Value);
                ViewBag.CategoryId = categoryId.Value;
            }
            else
            {
                allProducts = DbHelper.GetAllProducts();
            }

            int totalProducts = allProducts.Count;
            var productsToShow = allProducts
                .Take(page * pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalProducts = totalProducts;

            return View(productsToShow);
        }

    }
}
