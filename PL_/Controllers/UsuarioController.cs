using Microsoft.AspNetCore.Mvc;

namespace PL_.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly BL.Usuario _usuario;
        private readonly BL.Rol _rol;

        public UsuarioController(BL.Usuario usuario, BL.Rol rol)
        {
            _usuario = usuario;
            _rol = rol;
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

        //[HttpGet]
        //public IActionResult GetAll()
        //{

        //    ML.Usuario usuario = new ML.Usuario();
        //    usuario.Nombre = "";
        //    usuario.ApellidoPaterno = "";
        //    usuario.ApellidoMaterno = "";


        //    ML.Result result = _usuario.GetAll();
        //    ML.Result resultRol = _rol.GetAll();


        //    if (result.Correct.HasValue)
        //    {
        //        usuario.Usuarios = result.Objects;
        //        usuario.Rol = new ML.Rol();
        //        usuario.Rol.Roles = resultRol.Objects;
        //    }

        //    return View(usuario);
        //}

    }
}
