using Microsoft.AspNetCore.Mvc;

namespace PL_.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly BL.Usuario _usuario;

        public UsuarioController(BL.Usuario usuario)
        {
            _usuario = usuario;
        }




        //public IActionResult GetAll()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult GetAll()
        {

            ML.Usuario usuario = new ML.Usuario();
            //ML.Result result = _restaurante.GetAll();
            ML.Result result = _usuario.GetAll();
            if (result.Correct.HasValue)
            {
                usuario.Usuarios = result.Objects;
            }
            return View(usuario);

        }



    }
}
