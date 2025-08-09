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

                string body = "<!DOCTYPE html>    \r\n<html>    \r\n<head>\r\n<title>Burger Stand</title>\r\n<link rel=\"shortcut icon\" href=\"favicon.ico\">\r\n<style type=\"text/css\">\r\ntable[name=\"blk_permission\"], table[name=\"blk_footer\"] {display:none;} \r\n</style>\r\n<meta name=\"googlebot\" content=\"noindex\" />\r\n<META NAME=\"ROBOTS\" CONTENT=\"NOINDEX, NOFOLLOW\"/>    \r\n<meta content=\"width=device-width, initial-scale=1.0\" name=\"viewport\">       \r\n</head>    \r\n<body marginheight=0 marginwidth=0 topmargin=0 leftmargin=0 style=\"height: 100% !important; margin: 0; padding: 0; width: 100% !important;min-width: 100%;\">    \r\n    \r\n<table name=\"bmeMainBody\" style=\"background-color: rgb(214, 71, 69);\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" bgcolor=\"#d64745\"><tbody><tr><td width=\"100%\" valign=\"top\" align=\"center\"><table name=\"bmeMainColumnParentTable\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td name=\"bmeMainColumnParent\" style=\"border: 0px none transparent; border-radius: 0px; border-collapse: separate; border-spacing: 0px;\"> <table name=\"bmeMainColumn\" class=\"\" style=\"max-width: 100%;\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" align=\"center\">    <tbody><tr><td class=\"blk_container bmeHolder\" name=\"bmePreHeader\" style=\"color: rgb(102, 102, 102); border: 0px none transparent;\" width=\"100%\" valign=\"top\" bgcolor=\"\" align=\"center\"><div id=\"dv_2\" class=\"blk_wrapper\"><table class=\"blk\" name=\"blk_permission\" style=\"\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td name=\"tblCell\" style=\"padding:20px;\" valign=\"top\" align=\"left\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td name=\"bmePermissionText\" style=\"text-align:left;\" align=\"left\"><span style=\"font-family: Arial, Helvetica, sans-serif; font-weight: normal; font-size: 11px;line-height: 140%;\"><a style=\"color: #16a7e0;\" target=\"_new\" href=\"[ViewWebURL]\">View this email in your browser</a></span></td></tr></tbody></table></td></tr></tbody></table></div></td></tr> <tr><td class=\"bmeHolder\" name=\"bmeMainContentParent\" style=\"border: 0px none transparent; border-radius: 0px; border-collapse: separate; border-spacing: 0px; overflow: hidden;\" width=\"100%\" valign=\"top\" align=\"center\"> <table name=\"bmeMainContent\" style=\"border-radius: 0px; border-collapse: separate; border-spacing: 0px; border: 0px none transparent;\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" align=\"center\"> <tbody><tr><td class=\"blk_container bmeHolder\" name=\"bmeHeader\" style=\"border: 0px none transparent;\" width=\"100%\" valign=\"top\" bgcolor=\"\" align=\"center\"><div id=\"dv_1\" class=\"blk_wrapper\"><table class=\"blk\" name=\"blk_divider\" style=\"\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td class=\"tblCellMain\" style=\"padding: 30px 20px;\"><table class=\"tblLine\" style=\"border-top-width: 0px; border-top-style: none; min-width: 560px;\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td><span></span></td></tr></tbody></table></td></tr></tbody></table></div>\r\n</td></tr> <tr><td class=\"blk_container bmeHolder\" name=\"bmeBody\" style=\"color: rgb(56, 56, 56); border: 0px none transparent; background-color: rgb(255, 246, 239);\" width=\"100%\" valign=\"top\" bgcolor=\"#fff6ef\" align=\"center\"><div id=\"dv_3\" class=\"blk_wrapper\"><table class=\"blk\" name=\"blk_divider\" style=\"\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td class=\"tblCellMain\" style=\"padding-top:20px; padding-bottom:20px;padding-left:20px;padding-right:20px;\"><table class=\"tblLine\" style=\"border-top-width: 0px; border-top-style: none; min-width: 560px;\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td><span></span></td></tr></tbody></table></td></tr></tbody></table></div><div id=\"dv_9\" class=\"blk_wrapper\"><table class=\"blk\" name=\"blk_image\" style=\"\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td class=\"bmeImage\" style=\"padding: 0px;\" align=\"center\"><img src=\"https://www.benchmarkemail.com/images/templates_n/new_editor/Templates/Burgerstand/labeltop.png\" style=\"max-width: 1200px; display: block;\" alt=\"\" width=\"600\" border=\"0\"></td></tr></tbody></table></div><div id=\"dv_11\" class=\"blk_wrapper\"><table class=\"blk\" name=\"blk_image\" style=\"\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td class=\"bmeImage\" style=\"padding: 0px;\" align=\"center\"><img src=\"https://www.benchmarkemail.com/images/templates_n/new_editor/Templates/Burgerstand/logo.png\" style=\"max-width: 90px; display: block;\" alt=\"\" width=\"90\" border=\"0\"></td></tr></tbody></table></div><div id=\"dv_12\" class=\"blk_wrapper\"><table class=\"blk\" name=\"blk_image\" style=\"\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td class=\"bmeImage\" style=\"padding: 0px;\" align=\"center\"><img src=\"https://www.benchmarkemail.com/images/templates_n/new_editor/Templates/Burgerstand/buntop.png\" style=\"max-width: 1200px; display: block;\" alt=\"\" width=\"600\" border=\"0\"></td></tr></tbody></table></div><div id=\"dv_13\" class=\"blk_wrapper\"><table class=\"blk\" name=\"blk_text\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td><table class=\"bmeContainerRow\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td class=\"tdPart\" valign=\"top\" align=\"center\"><table name=\"tblText\" style=\"float:left; background-color:transparent;\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" align=\"left\"><tbody><tr><td name=\"tblCell\" style=\"padding: 0px 20px; font-family: Arial, Helvetica, sans-serif; font-size: 14px; font-weight: normal; color: rgb(56, 56, 56); text-align: left;\" valign=\"top\" align=\"left\"><div style=\"line-height: 150%; text-align: center;\"><span style=\"font-size: 30px; font-family: Tahoma, Arial, Helvetica, sans-serif; color: #bd3e3d; line-height: 150%;\"><strong>Come Join Us</strong></span></div></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></div><div id=\"dv_14\" class=\"blk_wrapper\"><table class=\"blk\" name=\"blk_text\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td><table class=\"bmeContainerRow\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td class=\"tdPart\" valign=\"top\" align=\"center\"><table name=\"tblText\" style=\"float:left; background-color:transparent;\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" align=\"left\"><tbody><tr><td name=\"tblCell\" style=\"padding: 0px 20px; font-family: Arial, Helvetica, sans-serif; font-size: 14px; font-weight: normal; color: rgb(56, 56, 56); text-align: left;\" valign=\"top\" align=\"left\"><div style=\"line-height: 150%; text-align: center;\"><span style=\"font-size: 14px; font-family: Tahoma, Arial, Helvetica, sans-serif; color: #c4c163; line-height: 150%;\">Bring your friends and family to Sonny's on June 8. Every person who attends gets to sample all five of our new menu items.</span></div></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></div><div id=\"dv_16\" class=\"blk_wrapper\"><table class=\"blk\" name=\"blk_button\" style=\"\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td width=\"20\"></td><td align=\"center\"><table class=\"tblContainer\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td height=\"10\"></td></tr><tr><td align=\"left\"><table class=\"bmeButton\" style=\"border-collapse: separate;\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td style=\"border-radius: 20px; border: 2px dashed rgb(144, 216, 189); text-align: center; border-collapse: separate;font-family: Arial, Helvetica, sans-serif; font-size: 14px; padding: 5px 40px; font-weight: bold;\" class=\"bmeButtonText\"><span style=\"font-family: Tahoma, Arial, Helvetica, sans-serif; font-size: 20px; color: rgb(0, 127, 126);\"><a style=\"color: rgb(0, 127, 126); text-decoration: none;\" target=\"_blank\">Saturday, June 8th from 2pm to 4pm</a></span></td></tr></tbody></table></td></tr><tr><td height=\"10\"></td></tr></tbody></table></td><td width=\"20\"></td></tr></tbody></table></div><div id=\"dv_17\" class=\"blk_wrapper\"><table class=\"blk\" name=\"blk_image\" style=\"\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td class=\"bmeImage\" style=\"padding: 0px;\" align=\"center\"><img src=\"https://www.benchmarkemail.com/images/templates_n/new_editor/Templates/Burgerstand/bacon.png\" style=\"max-width: 1200px; display: block;\" alt=\"\" width=\"600\" border=\"0\"></td></tr></tbody></table></div><div id=\"dv_18\" class=\"blk_wrapper\"><table class=\"blk\" name=\"blk_image\" style=\"\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td class=\"bmeImage\" style=\"padding: 0px;\" align=\"center\"><img src=\"https://www.benchmarkemail.com/images/templates_n/new_editor/Templates/Burgerstand/cheese.png\" style=\"max-width: 263px; display: block;\" alt=\"\" width=\"263\" border=\"0\"></td></tr></tbody></table></div><div id=\"dv_19\" class=\"blk_wrapper\"><table class=\"blk\" name=\"blk_button\" style=\"\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td width=\"20\"></td><td align=\"center\"><table class=\"tblContainer\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td height=\"10\"></td></tr><tr><td align=\"left\"><table class=\"bmeButton\" style=\"border-collapse: separate;\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td style=\"border-radius: 20px; border: 7px dotted rgb(126, 42, 41); text-align: center; border-collapse: separate; background-color: rgb(88, 29, 28);font-family: Arial,Helvetica,sans-serif; font-size: 14px; padding-top:20px;padding-bottom:20px; padding-left:40px; padding-right:40px;font-weight:bold;\" class=\"bmeButtonText\"><span style=\"font-family: Tahoma, Arial, Helvetica, sans-serif; font-size: 20px; color: rgb(255, 255, 255);\"><a style=\"color: rgb(255, 255, 255); text-decoration: none;\" target=\"_blank\">Pre-Order a Plate</a></span></td></tr></tbody></table></td></tr><tr><td height=\"10\"></td></tr></tbody></table></td><td width=\"20\"></td></tr></tbody></table></div><div id=\"dv_20\" class=\"blk_wrapper\"><table class=\"blk\" name=\"blk_image\" style=\"\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td class=\"bmeImage\" style=\"padding: 0px;\" align=\"center\"><img src=\"https://www.benchmarkemail.com/images/templates_n/new_editor/Templates/Burgerstand/bunbottom.png\" style=\"max-width: 1200px; display: block;\" alt=\"\" width=\"600\" border=\"0\"></td></tr></tbody></table></div><div id=\"dv_21\" class=\"blk_wrapper\"><table class=\"blk\" name=\"blk_divider\" style=\"\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td class=\"tblCellMain\" style=\"padding: 15px 20px;\"><table class=\"tblLine\" style=\"border-top-width: 0px; border-top-style: none; min-width: 560px;\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td><span></span></td></tr></tbody></table></td></tr></tbody></table></div>\r\n\r\n\r\n</td></tr> <tr><td class=\"blk_container bmeHolder\" name=\"bmePreFooter\" style=\"border: 0px none transparent; background-color: rgb(255, 246, 239);\" width=\"100%\" valign=\"top\" bgcolor=\"#fff6ef\" align=\"center\"><div id=\"dv_4\" class=\"blk_wrapper\"><table class=\"blk\" name=\"blk_social_share\" style=\"\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td class=\"tblCellMain\" style=\"padding-top:10px; padding-bottom:10px; padding-left:20px; padding-right:20px;\"><table class=\"tblContainer mblSocialContain bmeMblShareCenter\" style=\"text-align: center; margin: 0px auto;\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" align=\"center\"><tbody><tr><td class=\"tdItemContainer\" align=\"center\"><table cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td name=\"bmeShareTD\" class=\"bmeMblStackCenter\" valign=\"top\"><table class=\"bmeShareItem\" type=\"twitter\" style=\"float: left; table-layout: auto; display: block;\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" align=\"left\"><tbody><tr><td align=\"center\"><table class=\"bmeShareItemBtn\" style=\"display: inline-block;background-color: rgb(50, 203, 255);color: rgb(255, 255, 255);border-radius: 4px;\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td style=\"padding: 5px 15px;\"><table style=\"table-layout:auto;\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td width=\"20\" align=\"left\"><a href=\"[ShareTwitterURL]\" target=\"_blank\"><img src=\"https://ui.benchmarkemail.com/images/editor/socialicons/tw_icn2.png\" alt=\"Twitter\" style=\"display: block; max-width: 32px;\" width=\"16\" height=\"16\" border=\"0\"></a></td><td width=\"10\"><img src=\"https://www.benchmarkemail.com/images/blank.gif\" style=\"max-width: 1px; display: block;\" width=\"10\" height=\"5\" border=\"0\"></td><td class=\"bmeShareItemText\" style=\"font-family: Helvetica, Calibri, Arial, sans-serif; font-size: 12px; color: rgb(255, 255, 255);\"><a href=\"[ShareTwitterURL]\" style=\"text-decoration:none;\" target=\"_blank\"><span style=\"font-family: Helvetica, Calibri, Arial, sans-serif; font-size: 12px; font-weight: normal; font-style: normal; text-decoration: none; color: rgb(255, 255, 255);\">Share</span></a></td></tr></tbody></table></td></tr></tbody></table></td><td gutter=\"10\" class=\"tdMblHide\" width=\"10\"></td></tr><tr class=\"trMargin\"><td colspan=\"2\"></td></tr></tbody></table><!--[if gte mso 6]></td><td align=\"left\" valign=\"top\"><![endif]--><table class=\"bmeShareItem\" type=\"facebook\" style=\"float: left; table-layout: auto; display: block;\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" align=\"left\"><tbody><tr><td align=\"center\"><table class=\"bmeShareItemBtn\" style=\"display: inline-block;background-color: rgb(53, 91, 161);color: rgb(255, 255, 255);border-radius: 4px;\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td style=\"padding: 5px 15px;\"><table style=\"table-layout:auto;\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td width=\"20\" align=\"left\"><a href=\"[ShareFacebookURL]\" target=\"_blank\"><img src=\"https://ui.benchmarkemail.com/images/editor/socialicons/fb_icn_new.png\" alt=\"Facebook\" style=\"display: block; max-width: 32px;\" width=\"16\" height=\"16\" border=\"0\"></a></td><td width=\"10\"><img src=\"https://www.benchmarkemail.com/images/blank.gif\" style=\"max-width: 1px; display: block;\" width=\"10\" height=\"5\" border=\"0\"></td><td class=\"bmeShareItemText\" style=\"font-family: Helvetica, Calibri, Arial, sans-serif; font-size: 12px; color: rgb(255, 255, 255);\"><a href=\"[ShareFacebookURL]\" style=\"text-decoration:none;\" target=\"_blank\"><span style=\"font-family: Helvetica, Calibri, Arial, sans-serif; font-size: 12px; font-weight: normal; font-style: normal; text-decoration: none; color: rgb(255, 255, 255);\">Share</span></a></td></tr></tbody></table></td></tr></tbody></table></td><td gutter=\"10\" class=\"tdMblHide\" width=\"10\"></td></tr><tr class=\"trMargin\"><td colspan=\"2\"></td></tr></tbody></table><!--[if gte mso 6]></td><td align=\"left\" valign=\"top\"><![endif]--></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></div><div id=\"dv_7\" class=\"blk_wrapper\"><table name=\"blk_divider\" class=\"blk\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td style=\"padding: 20px;\" class=\"tblCellMain\"><table style=\"border-top-width: 0px; border-top-style: none; min-width: 560px;\" class=\"tblLine\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td><span></span></td></tr></tbody></table></td></tr></tbody></table></div>\r\n\r\n</td></tr> </tbody></table> </td></tr>  <tr><td class=\"blk_container bmeHolder\" name=\"bmeFooter\" style=\"color: rgb(102, 102, 102); border: 0px none transparent;\" width=\"100%\" valign=\"top\" bgcolor=\"\" align=\"center\"><div id=\"dv_10\" class=\"blk_wrapper\"><table class=\"blk\" name=\"blk_footer\" style=\"\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td name=\"tblCell\" style=\"padding:20px;\" valign=\"top\" align=\"left\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><td name=\"bmeBadgeText\" style=\"text-align: center;\" align=\"center\"><span id=\"spnFooterText\" style=\"font-family: Arial, Helvetica, sans-serif; font-weight: normal; font-size: 11px; line-height: 140%; color: rgb(255, 255, 255);\"><var type=\"BME_CANSPAM\">This message was sent to test@benchmarkemail.com by <span style=\"text-decoration:underline;\">Benchmark Templates</span></var><br><var type=\"BME_ADDRESS\">10621 Calle Lee, Building 141, Los Alamitos, CA, 90720</var></span><br><br><span style=\"font-family: Arial, Helvetica, sans-serif; font-weight: normal; font-size: 11px; line-height: 140%; color: rgb(255, 255, 255);\"><var type=\"BME_LINKS\"><span style=\"text-decoration:underline\" href=\"#\"><img src=\"https://www.benchmarkemail.com/images/verified.png\" alt=\"Unsubscribe from all mailings\" title=\"Unsubscribe from all mailings\" border=\"0\"></span><span style=\"text-decoration:underline;\">Unsubscribe</span> | <span style=\"text-decoration:underline;\">Manage Subscription</span> |  <span style=\"text-decoration:underline;\">Forward Email</span>  |  <span style=\"text-decoration:underline;\">Report Abuse</span></var><br></span></td></tr><tr><td name=\"bmeBadgeImage\" style=\"text-align: center; padding-top: 20px; word-break: break-all;\" align=\"center\"><var type=\"BME_BADGE\"><img src=\"https://www.benchmarkemail.com/images/web4/misc/emailfooter/opt9.png\" name=\"bmeBadgeImage\" border=\"0\"></var></td></tr></tbody></table></td></tr></tbody></table></div></td></tr> </tbody></table></td></tr></tbody></table></td></tr></tbody></table>    \r\n</body>    \r\n</html><img    \r\n src='http://benchmarkemail.benchurl.com/c/o?e=8E4B52&c=91CEA&t=1&l=7889F345&email=hL2iimIGZvj2QooSzVze1t7P%2FZjRPRKrj2c0%2B7DqUhU%3D&relid=' alt='' border=0 style=\"display:none;\" height=1 width=1>        \r\n   \r\n";
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
            if (result.Correct.HasValue)
            {
                EnviarCorreo();
                return RedirectToAction("GetAll");
            }
            return null;
        }

    }
}
