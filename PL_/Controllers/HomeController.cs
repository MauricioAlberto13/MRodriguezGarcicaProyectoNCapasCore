using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL_.Models;

namespace PL_.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Authorize]
    public IActionResult Index()
    {

     //   var identity = HttpContext.User.Identity as ClaimsIdentity;

        //if(identity != null){
        //    var nombre = identity.FindFirst("Name").Value;
        //    var rol = identity.FindFirst("Role").Value;
        //    var objeto = new
        //    {
        //        nombre,
        //        rol
        //    };
        //    return Ok(objeto);
        //}



        return View();
    
    }
    public IActionResult AccessDenied()
    {
        return View();
    }



    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
