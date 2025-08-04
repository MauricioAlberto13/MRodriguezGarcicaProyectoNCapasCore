using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL_.Controllers
{
    public class LoginController : Controller
    {


        private readonly string _usuarioEndpointLogin;

        public LoginController( IConfiguration configuration)
        {
            _usuarioEndpointLogin = configuration["AppSettings:UsuarioLogin"];
        }


        [HttpGet]
        [AllowAnonymous]

        public IActionResult Login()
        {
            ML.Login login = new ML.Login();



            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public IActionResult Login(ML.Login login)
        //{
        //    using (var client = new HttpClient())
        //    {

        //        string Endpoint = (_usuarioEndpointLogin);
        //        client.BaseAddress = new Uri(Endpoint);
        //        //HTTP POST($"GetById/{IdUsuario}");
        //        //HTTP POST            

        //        //var postTask = client.PostAsJsonAsync();
        //        var postTask = client.PostAsJsonAsync<ML.Login>("LoginUsuario", login);

        //        postTask.Wait();



        //        var result = postTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsStringAsync();
        //            string token = readTask.Result;


        //            //Aqui va lo de las cookies
        //            HttpContext.Response.Cookies.Append("session",token, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddMinutes(5)});
        //            return RedirectToAction("Index","Home");
        //        }

        //    }
        //    //return RedirectToAction("GetAll", "Usuario");

        //    return View("GetAll");
        //    //return View("Login");
        //}



        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(ML.Login login)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_usuarioEndpointLogin);

                var postTask = client.PostAsJsonAsync("LoginUsuario", login);
                
                postTask.Wait();
                var result = postTask.Result; 
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync(); 
                    
                    readTask.Wait();
                    var json = readTask.Result; 
                    var obj = System.Text.Json.JsonDocument.Parse(json); 


                    string token = obj.RootElement.GetProperty("token").GetString();


                    HttpContext.Response.Cookies.Append("session", token, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddMinutes(5) });


                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    login.Password = null;
                    ViewBag.MensajeError = "Usuario o contraseña incorrectos";
                    
                    return View(new ML.Login());
                } 
            }
            
        
        }


         }
    }
