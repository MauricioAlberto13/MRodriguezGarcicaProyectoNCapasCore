using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ML;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SL_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly BL.Usuario _usuario;
        private readonly BL.Rol _rol;
        private readonly BL.Colonia _colonia;
        private readonly BL.Municipio _municipio;
        private readonly BL.Estado _estado;
        private readonly BL.Login _BLlogin;

        public UsuarioController(BL.Usuario usuario, BL.Rol rol, BL.Colonia colonia, BL.Municipio municipio, BL.Estado estado, BL.Login bLlogin)
        {
            _usuario = usuario;
            _rol = rol;
            _colonia = colonia;
            _municipio = municipio;
            _estado = estado;
            _BLlogin = bLlogin;
        }
        [HttpPost]
        [Route("Add")]
  
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {
            usuario.Imagen = Convert.FromBase64String(usuario.ImagenBase64);
            usuario.ImagenBase64 = "";

            ML.Result result = _usuario.Add(usuario);
            if (result.Correct.HasValue)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);

            }
        }

        [HttpPut]
        [Route("Update/{IdUsuario}")]
        public IActionResult Update(int IdUsuario, [FromBody] ML.Usuario usuario)


        {
            usuario.IdUsuario = IdUsuario;

            //usuario.Imagen = Convert.FromBase64String(usuario.ImagenBase64);
            //usuario.ImagenBase64 = "";

            ML.Result result = _usuario.Update(usuario);
            if (result.Correct.HasValue)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }


        }




        [HttpGet]
        [Route("GetAll")]
        [Authorize]

        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();


            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;
            ML.Result result = _usuario.GetAllV(usuario);
            if (result.Correct.HasValue)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }



       
        [HttpPost]
        [Route("BusquedaAbierta")]
        public IActionResult BusquedaAbierta([FromBody] ML.Usuario usuario)
        {

            usuario.Rol = new ML.Rol();


            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;

            ML.Result result = _usuario.GetAllV(usuario);
            if (result.Correct.HasValue)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        [Route("GetById/{IdUsuario}")]
        public IActionResult GetById(int IdUsuario)
        {
            ML.Result result = _usuario.GetById(IdUsuario);
            if (result.Correct.HasValue)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }



        [HttpDelete]
        [Route("Delete/{IdUsuario}")]
        public IActionResult Delete(int IdUsuario)
        {
            ML.Result result = _usuario.Delete(IdUsuario);
            if (result.Correct.HasValue)
            {
                return Ok( result);
            }
            else
            {
                return BadRequest( result);
            }

        }



    }
}
