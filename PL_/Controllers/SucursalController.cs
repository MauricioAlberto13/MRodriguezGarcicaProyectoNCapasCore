using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL_.Controllers
{
    public class SucursalController : Controller
    {




        private readonly BL.ProductoSucursal _productoSucursal;
        private readonly BL.Sucursal _sucursal;


        private readonly IHostingEnvironment _env;


        public SucursalController(BL.ProductoSucursal productoSucursal, BL.Sucursal sucursal,IHostingEnvironment env)
        {
            _productoSucursal= productoSucursal;
            _sucursal= sucursal;
            _env = env;

        }


        [HttpGet]
        public IActionResult GetAll()
        {
            ML.ProductoSucursal productoSucursal= new ML.ProductoSucursal();
            productoSucursal.Sucursal = new ML.Sucursal();
            ML.Result resultSu = _sucursal.GetAll();
            ML.Result result = _productoSucursal.GetAll();
              
            if (result.Correct.HasValue)
            {
                productoSucursal.ProductoSucursales = result.Objects;
                productoSucursal.Sucursal.Sucursales = resultSu.Objects;
               
            }

            return View(productoSucursal);
        }


        [HttpPost]
        public IActionResult GetAll(ML.ProductoSucursal productoSucursal)
        {
            // ML.Producto producto = new ML.Producto();
            //ML.Result result = _restaurante.GetAll();
            //producto.SubCategoria = new ML.SubCategoria();
            //producto.SubCategoria.Categoria = new ML.Categoria();



            ML.Result result = _productoSucursal.GetAllV(productoSucursal);
            // ML.Result result = _producto.GetAll();
            if (result.Correct.HasValue)
            {
                productoSucursal.ProductoSucursales = result.Objects;
            }
            ML.Result resultCategoria = _sucursal.GetAll();
            if (resultCategoria.Correct.HasValue)
            {
               productoSucursal.Sucursal.Sucursales= resultCategoria.Objects;
            }
            return View(productoSucursal);
        }

        public IActionResult? Delete(int IdProducto)

        {

            ML.Result result = _productoSucursal.Delete2(IdProducto);
            if (result.Correct.HasValue)
            {
                return RedirectToAction("GetAll");
            }
            return null;
        }



        //public IActionResult GetAll()
        //{
        //    return View();
        //}

        [NonAction]
        public ML.Result EnviarCorreo()
        {
            ML.Result result = new ML.Result();
            try
            {

                var identity = HttpContext.User.Identity as ClaimsIdentity;
                
                if (identity != null)
                {
                var nombre= identity.Name;
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                string correo="alber13boy@gmail.com";
                string password= "phqg wmnc wtsr vorz";

                string body = "";
                //string path = ("~/Content/ArchivosTxt/Errores/");

                //StreamReader reader = new StreamReader(path);
                //body = reader.ReadToEnd();
                body = body.Replace("{{NombreUsuario}}", nombre);
                body = body.Replace("{{LINK}}", Url.Action("Index","Home"));

                var smptClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(correo, password),
                    EnableSsl = true
                };

                var message = new MailMessage
                {
                    From = new MailAddress(correo,"Mauricio Alb"),
                    Subject ="Actualización de Stock",
                    Body = body,
                    IsBodyHtml=false
                };
                message.To.Add(email);
                smptClient.Send(message);
                }
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }

        public IActionResult? UpdateStock(ML.ProductoSucursal productoSucursal)

        {

            ML.Result result = _productoSucursal.Update(productoSucursal);
            if (result.Correct.HasValue)
            {
                EnviarCorreo();
                return RedirectToAction("GetAll");
            }
            return null;
        }

    }
}
