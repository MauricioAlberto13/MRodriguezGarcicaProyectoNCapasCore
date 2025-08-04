using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SL_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly BL.Login _BLlogin;

        public LoginController(BL.Login BLlogin)
        {
            _BLlogin = BLlogin;
        }


        [HttpPost("LoginUsuario")]
        public IActionResult Login([FromBody] ML.Login login)
        {

            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = _BLlogin.Loggin(login);
            if ((bool)result.Correct)
            {

                usuario = (ML.Usuario)result.Object;
                var token = GenerateJwtToken(usuario);


                return Ok(new { token });

            }
            else
            {
         

                return BadRequest(result);

            }
        }

        private string GenerateJwtToken(ML.Usuario usuario)
        {

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, usuario.Rol.NombreR),
                new Claim(ClaimTypes.Name, usuario.Nombre)
            //new Claim(JwtRegisteredClaimNames.Sub, username),
            //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("69a0921dee7f1e1fd8e995619945c803"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
