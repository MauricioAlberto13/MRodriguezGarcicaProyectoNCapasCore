using Microsoft.AspNetCore.Mvc;

namespace PL_.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly BL.Usuario _usuario;
        private readonly BL.Rol _rol;
        private readonly BL.Colonia _colonia;
        private readonly BL.Municipio _municipio;
        private readonly BL.Estado _estado;

        public UsuarioController(BL.Usuario usuario, BL.Rol rol, BL.Colonia colonia, BL.Municipio municipio, BL.Estado estado)
        {
            _usuario = usuario;
            _rol = rol;
            _colonia = colonia;
            _municipio = municipio;
            _estado = estado;
        }




        [HttpGet]
        public IActionResult GetAll()
        {

            //ML.Usuario usuario = new ML.Usuario();
            ////ML.Result result = _restaurante.GetAll();
            //ML.Result result = _usuario.GetAll();
            //if (result.Correct.HasValue)
            //{
            //    usuario.Usuarios = result.Objects;
            //}
            //return View(usuario);



            ML.Usuario usuario = new ML.Usuario();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";

            ML.Result result = _usuario.GetAllV(usuario);
            ML.Result resultRol = _rol.GetAll();


            if (result.Correct.HasValue)
            {
                usuario.Usuarios = result.Objects;
                usuario.Rol = new ML.Rol();
                usuario.Rol.Roles = resultRol.Objects;
            }

            return View(usuario);
        }

    


        [HttpGet]
        public IActionResult Delete(ML.Usuario usuario)
        {


            ML.Result result = _usuario.Delete(usuario.IdUsuario);
            if (result.Correct.HasValue)
            {
                return RedirectToAction("GetAll");
            }
            return View();

        }
        [HttpPost]
        public JsonResult CambiarStatus(int IdUsuario, bool Status)
        {
            ML.Result result = _usuario.CambiarStatus(IdUsuario, Status);


            //return Json(result, JsonRequestBehavior.AllowGet);
            return Json(new { success = result.Correct });
        }


        [HttpGet]
        public IActionResult Form(int? IdUsuario)
        {


            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

            ML.Result resultRol =_rol.GetAll();
            if (resultRol.Correct.HasValue)
            {
                usuario.Rol.Roles = resultRol.Objects;
            }
            ML.Result resultEstado = _estado.GetAll();
            if (resultEstado.Correct.HasValue)
            {
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
            }

            if (IdUsuario > 0)
            {
                //Esto es haciendo uso del web service con SOAP

                //UsuarioReference.UsuarioClient usuarioSOAP = new UsuarioReference.UsuarioClient();
                //var respuesta = usuarioSOAP.GetById(IdUsuario.Value);

                //Con soap
                // usuario = GetBySoapUsuario(IdUsuario.Value);


                // ML.Result result = GetByIdWebAPI(IdUsuario.Value);
                ML.Result result =_usuario.GetById(IdUsuario.Value);

                if (result.Correct.HasValue)
                {
                    usuario = (ML.Usuario)result.Object;


                    //
                    if (resultEstado.Correct.HasValue)
                    {
                        usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    }
                    ML.Result municipios =_municipio.GetIdMunicipio((int)usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    if (municipios.Correct.HasValue)
                    {
                        usuario.Direccion.Colonia.Municipio.Municipios = municipios.Objects;
                    }

                    ML.Result colonias = _colonia.GetColoniaByIdMunicipio((int)(usuario.Direccion.Colonia.Municipio.IdMunicipio));
                    if (colonias.Correct.HasValue)
                    {
                        usuario.Direccion.Colonia.Colonias = colonias.Objects;
                    }


                    if (resultRol.Correct.HasValue)
                    {
                        usuario.Rol.Roles = resultRol.Objects;
                    }
                    usuario.Sexo = usuario.Sexo?.Trim().ToUpper();

                }

            }

            if (resultRol.Correct.HasValue)
            {
                usuario.Rol.Roles = resultRol.Objects;
            }

            return View(usuario);
        }

        [HttpPost]
        public IActionResult Form(ML.Usuario usuario, IFormFile? imagenUser)
        {
            if (ModelState.IsValid)
            {
                if (imagenUser != null && imagenUser.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        imagenUser.CopyTo(memoryStream);
                        usuario.Imagen = memoryStream.ToArray();
                    }
                }
                if (usuario.IdUsuario > 0)
                {
        
                    ML.Result result = _usuario.Update(usuario);
                     if (!result.Correct.HasValue)
                    {
                        return View(usuario);
                    }
                }
                else
                    {
                      ML.Result result = _usuario.Add(usuario);
                    if (!result.Correct.HasValue)
                    {
                        return View(usuario);
                    }
                }

                usuario.Rol = new ML.Rol();
                ML.Result resultRol = _rol.GetAll();
                if (resultRol.Correct.HasValue)
                  {
                       usuario.Rol.Roles = resultRol.Objects;
                }

                 ML.Result resultEstado = _estado.GetAll();
                 if (resultEstado.Correct.HasValue)
                {
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                }
            }
            else
            {
                usuario.Rol = new ML.Rol();
                ML.Result resultRol = _rol.GetAll();
                if (resultRol.Correct.HasValue)
                {
                    usuario.Rol.Roles = resultRol.Objects;
                }

                ML.Result resultEstado = _estado.GetAll();
                if (resultEstado.Correct.HasValue)
                {
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                }

                 ML.Result municipios = _municipio.GetIdMunicipio((int)(usuario.Direccion.Colonia.Municipio.Estado.IdEstado));
                     if (municipios.Correct.HasValue)
                {
                    usuario.Direccion.Colonia.Municipio.Municipios = municipios.Objects;
                }

                ML.Result colonias =_colonia.GetColoniaByIdMunicipio((int)(usuario.Direccion.Colonia.Municipio.IdMunicipio));
                 if (colonias.Correct.HasValue)
                {
                    usuario.Direccion.Colonia.Colonias = colonias.Objects;
                }

                return View(usuario);
            }

            return RedirectToAction("GetAll");
        }


        [HttpGet]
        public JsonResult GetMunicipioByIdEstado(int IdEstado)
        {
            var resultMunicipios = _municipio.GetIdMunicipio(IdEstado);
              return new JsonResult(resultMunicipios);
        }


        [HttpGet]
        public JsonResult GetColoniaByIdMunicipio(int IdMunicipio)
        {
            var resultColonias = _colonia.GetColoniaByIdMunicipio(IdMunicipio);
            return new JsonResult(resultColonias);
        }


    }
}
