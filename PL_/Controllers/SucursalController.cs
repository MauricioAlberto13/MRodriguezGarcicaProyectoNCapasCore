using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Hosting;
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
        private readonly IConfiguration _config;

        private readonly IHostingEnvironment _env;


        public SucursalController(BL.ProductoSucursal productoSucursal, BL.Sucursal sucursal,IHostingEnvironment env, IConfiguration config)
        {
            _productoSucursal = productoSucursal;
            _sucursal = sucursal;
            _env = env;
            _config = config;
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
                    var emailUser = User.FindFirst(ClaimTypes.Email)?.Value;
                    var email = "mauricioalbertorg364@gmail.com";

                    //string correo="alber13boy@gmail.com";
                    //string password= "phqg wmnc wtsr vorz";       
                    string correo= _config["AppSettings:Correo"];
                    string password= _config["AppSettings:Pass"];

                    string body = "";
                    string contentRootPath = _env.ContentRootPath;

                    string webRootPath = _env.WebRootPath;
                    string path = Path.Combine(webRootPath, "templates", "email.html");

                    StreamReader reader = new StreamReader(path);
                    body = reader.ReadToEnd();
                    body = body.Replace("{{NombreUsuario}}", nombre);
                    body = body.Replace("{{emailUser}}", emailUser);
                //    body = body.Replace("{{LINK}}", Url.Action("http://localhost:5274/Producto/GetAllJS"));
                  
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
                    IsBodyHtml=true
                };
                message.To.Add(addresses: email);
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
            if ((bool)result.Correct)
            {
                EnviarCorreo();
                return RedirectToAction("GetAll");
            }
            return RedirectToAction("GetAll");
        }

    }
}
