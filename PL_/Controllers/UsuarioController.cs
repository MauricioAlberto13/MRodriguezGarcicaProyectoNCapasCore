using BL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;

using Microsoft.EntityFrameworkCore;
using ML;
using Newtonsoft.Json;
namespace PL_.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly BL.Usuario _usuario;
        private readonly BL.Rol _rol;
        private readonly BL.Colonia _colonia;
        private readonly BL.Municipio _municipio;
        private readonly BL.Estado _estado;
        private readonly string _usuarioEndpoint;

        public UsuarioController(BL.Usuario usuario, BL.Rol rol, BL.Colonia colonia, BL.Municipio municipio, BL.Estado estado, IConfiguration configuration)
        {
            _usuario = usuario;
            _rol = rol;
            _colonia = colonia;
            _municipio = municipio;
            _estado = estado;
            _usuarioEndpoint = configuration["AppSettings:UsuarioEndPoint"];
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

           // ML.Result result = _usuario.GetAllV(usuario);
            ML.Result resultRol = _rol.GetAll();

            ML.Result result = GetAllRest();
            if (result.Correct.HasValue)
            {
                usuario.Usuarios = result.Objects;
                usuario.Rol = new ML.Rol();
                usuario.Rol.Roles = resultRol.Objects;
            }


            return View(usuario);
        }



        [HttpPost]
        public IActionResult GetAll(ML.Usuario usuario, IFormFile archivo, string validar)
        {
            //if (usuario.Rol == null)
            //{
            //    usuario.Rol = new ML.Rol();

            //}
            ////else
            ////{
            ////    usuario.Rol.IdRol = 0;
            ////}
           // usuario.Rol = new ML.Rol();
            if (usuario.Rol.IdRol.HasValue)
            {
                usuario.Rol.IdRol = usuario.Rol.IdRol.Value;
            }
            else
            {
                usuario.Rol.IdRol = 0;
            }
            usuario.Nombre ??= "";
            usuario.ApellidoPaterno ??= "";
            usuario.ApellidoMaterno ??= "";

    

           ML.Result result =_usuario.GetAllV(usuario);
          //  ML.Result result =GetAllBusquedaAbierta(usuario);
            if (result.Correct.HasValue)
            {
                usuario.Usuarios = result.Objects;
            }

            usuario.Rol = new ML.Rol();
            ML.Result resultRol = _rol.GetAll();
            if (resultRol.Correct.HasValue)
            {
                usuario.Rol.Roles = resultRol.Objects;
            }

 
            return View(usuario);
        }

        //[HttpGet]
        //public IActionResult Delete(ML.Usuario usuario)
        //{


        //    ML.Result result = _usuario.Delete(usuario.IdUsuario);
        //    if (result.Correct.HasValue)
        //    {
        //        return RedirectToAction("GetAll");
        //    }
        //    return View();

        //}
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
        
                    //ML.Result result = _usuario.Update(usuario);
                    ML.Result result = UpdateRest(usuario);
                     if (!result.Correct.HasValue)
                    {
                        return View(usuario);
                    }
                }
                else
                    {
                      //ML.Result result = _usuario.Add(usuario);
                      ML.Result result = AddRest(usuario);
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






        public IActionResult? Delete(int IdUsuario)

        {

            //ML.Result result = _restaurante.Delete(IdRestaurante);


            //if (result.Correct)
            //{
            //    return RedirectToAction("GetAll");
            //}
            //return null;


            ML.Result result = DeleteApiRest(IdUsuario);
            if (result.Correct.HasValue)
            {
                return RedirectToAction("GetAll");
            }

            return null;

        }


        //[HttpGet]
        //public IActionResult? Form(int IdRestaurante)

        //{
        //    ML.Restaurante restaurante = new ML.Restaurante();

        //    //ML.Result result = _restaurante.Delete(IdRestaurante);

        //    if (IdRestaurante > 0)
        //    {

        //        // ML.Result result = _restaurante.GetById(IdRestaurante);
        //        ML.Result result = GetByIdRest(IdRestaurante);

        //        if (result.Correct)
        //        {
        //            restaurante = (ML.Restaurante)result.Object;
        //        }
        //    }

        //    return View(restaurante);

        //}

        //[HttpPost]
        //public IActionResult Form(ML.Restaurante restaurante, IFormFile? imagenRestaurante)
        //{
        //    if (imagenRestaurante != null && imagenRestaurante.Length > 0)
        //    {
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            imagenRestaurante.CopyTo(memoryStream);
        //            restaurante.Imagen = memoryStream.ToArray();
        //        }
        //    }
        //    if (restaurante.IdRestaurante > 0)
        //    {

        //        // ML.Result result = _restaurante.Update(restaurante);
        //        ML.Result result = UpdateRest(restaurante);

        //        if (!result.Correct)
        //        {
        //            return View(restaurante);
        //        }
        //    }
        //    else
        //    {
        //        // ML.Result result = _restaurante.Add(restaurante);
        //        ML.Result result = AddRest(restaurante);
        //        if (!result.Correct)
        //        {
        //            return View(restaurante);
        //        }


        //    }
        //    return RedirectToAction("GetAll");


        //}

        [NonAction]
        public ML.Result GetAllRest()
        {
            ML.Result result = new ML.Result();

            result.Objects = new List<Object>();
            try
            {
                using (var client = new HttpClient())
                {


                    client.BaseAddress = new Uri(_usuarioEndpoint);
                    var responseTask = client.GetAsync("GetAll");

                    responseTask.Wait(); //abrir otro hilo

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadFromJsonAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                        result.Correct = true;
                    }

                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        [NonAction]
        public ML.Result GetAllBusquedaAbierta(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result(); 
            result.Objects = new List<Object>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_usuarioEndpoint);
                    var postTask = client.PostAsJsonAsync("BusquedaAbierta", usuario);            
                    postTask.Wait();
                    var resultServicio = postTask.Result;
                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadFromJsonAsync<ML.Result>();
                        readTask.Wait();
                        foreach (var resultItem in readTask.Result.Objects)
                        { 
                            ML.Usuario resultItemList = JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString()); result.Objects.Add(resultItemList); 
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex) { result.ErrorMessage = ex.Message; result.Ex = ex; }
            return result;
        }
        [NonAction]
        public  ML.Result GetByIdRest(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var client = new HttpClient())
                {
                    /// string Endpoint = ConfigurationManager.AppSettings["UsuarioEndPoint"].ToString();
                    client.BaseAddress = new Uri(_usuarioEndpoint);

                    var responseTask = client.GetAsync($"GetById/{IdUsuario}");
                    responseTask.Wait();
                    var resultAPI = responseTask.Result;

                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadFromJsonAsync<ML.Result>();
                        readTask.Wait();
                        ML.Usuario resultItemList = new ML.Usuario();
                        resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existen registros en la tabla";
                    }

                }
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }
        [NonAction]
        private ML.Result DeleteApiRest(int IdUsuario)
        {

            ML.Result resultDelete = new ML.Result();

            using (var client = new HttpClient())
            {
                // string Endpoint = ConfigurationManager.["UsuarioEndPoint"].ToString();
         //       client.BaseAddress = new Uri("http://localhost:5052/api/Usuario/");

                client.BaseAddress = new Uri(_usuarioEndpoint);
                //HTTP POST
                var postTask = client.DeleteAsync("Delete/" + IdUsuario);

                //HTTP POST
                postTask.Wait();


                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    resultDelete.Correct = true;
                    //  return RedirectToAction("GetAll", resultListProduct);
                }
                else
                {
                    resultDelete.Correct = false;
                }

            }

            return resultDelete;
        }
        [NonAction]
        public ML.Result UpdateRest(ML.Usuario usuario)
        {
            ML.Result resultUpdate = new ML.Result();
            using (var client = new HttpClient())
            {

                //string Endpoint = ConfigurationManager.AppSettings["UsuarioEndPoint"].ToString();


              //  client.BaseAddress = new Uri("http://localhost:5052/api/Usuario/");

                client.BaseAddress = new Uri(_usuarioEndpoint);

                //HTTP POST

                var postTask = client.PutAsJsonAsync<ML.Usuario>($"Update/{usuario.IdUsuario}", usuario);
                postTask.Wait();



                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    resultUpdate.Correct = true;
                }
            }

            return resultUpdate;

        }
        [NonAction]
        public ML.Result AddRest(ML.Usuario usuario)
        {
            ML.Result resultAdd = new ML.Result();
            usuario.ImagenBase64 = Convert.ToBase64String(usuario.Imagen);
            usuario.Imagen = new byte[0];

            using (var client = new HttpClient())
            {

               // client.BaseAddress = new Uri(_usuarioEndpoint);

              //  string Endpoint = ("http://localhost:5052/api/Usuario/");
                string Endpoint = (_usuarioEndpoint);
                client.BaseAddress = new Uri(Endpoint);
                //HTTP POST
                //HTTP POST
                var postTask = client.PostAsJsonAsync<ML.Usuario>("Add", usuario);
                //Serializar
                postTask.Wait();



                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    resultAdd.Correct = true;
                }
                else
                {
                    resultAdd.Correct = false;
                }
            }

            return resultAdd;
        }


    }
}
