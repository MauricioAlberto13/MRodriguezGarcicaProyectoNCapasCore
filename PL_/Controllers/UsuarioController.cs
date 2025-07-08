using Microsoft.AspNetCore.Mvc;

namespace PL_.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult GetAll()
        {
            return View();
        }
    }
}
