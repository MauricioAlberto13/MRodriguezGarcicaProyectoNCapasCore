using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL_.Controllers
{
    public class LoginController : Controller
    {


        private readonly string _usuarioEndpoint;

        public LoginController( IConfiguration configuration)
        {
            _usuarioEndpoint = configuration["AppSettings:UsuarioLogin"];
        }


        [HttpGet]
        [AllowAnonymous]

        public IActionResult Login()
        {


            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(ML.Login login)
        {
            using (var client = new HttpClient())
            {

                // client.BaseAddress = new Uri(_usuarioEndpoint);

                //string Endpoint = ("http://localhost:5052/api/Usuario/");
                string Endpoint = (_usuarioEndpoint);
                client.BaseAddress = new Uri(Endpoint);
                //HTTP POST($"GetById/{IdUsuario}");
                //HTTP POST            

                //var postTask = client.PostAsJsonAsync();
                var postTask = client.PostAsJsonAsync<ML.Login>("LoginUsuario", login);
                //var postTask = client.PostAsJsonAsync<ML.Login>("http://localhost:5052/api/Login/LoginUsuario/", {login.Email});

                postTask.Wait();



                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    string token = readTask.Result;


                    //Aqui va lo de las cookies
                    HttpContext.Response.Cookies.Append("session",token, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddMinutes(5)});
                    return RedirectToAction("Index","Home");
                }
         
            }
            //return RedirectToAction("GetAll", "Usuario");

            return View("GetAll");
        }



    }
}
