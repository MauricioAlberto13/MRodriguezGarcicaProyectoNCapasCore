using Microsoft.AspNetCore.Mvc;

namespace PL_.Controllers
{
    public class Producto : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllProductos()
        {
            return View();
        }
    }
}
