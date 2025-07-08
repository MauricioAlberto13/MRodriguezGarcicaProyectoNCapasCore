using Microsoft.AspNetCore.Mvc;

namespace PL_.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly BL.Usuario _usuario;
        private readonly BL.Rol _rol;

        public UsuarioController(BL.Usuario usuario)
        {
            _usuario = usuario;
        }


        public UsuarioController(BL.Rol rol)
        {
            _rol = rol;
        }

        public IActionResult GetAll()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllV()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();

            usuario.Rol.IdRol = usuario.Rol.IdRol;

            ML.Result resultRol = _rol.GetAll();
            if ((bool)resultRol.Correct)
            {
                usuario.Rol.Roles = resultRol.Objects;
            }
            ML.Result result = _usuario.GetAll(usuario);
            if ((bool)result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            return View(usuario);

        }



    }
}
