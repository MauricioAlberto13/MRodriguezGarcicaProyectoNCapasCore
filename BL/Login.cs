using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Login
    {
        private readonly MrodriguezProgramacionNcapasContext _context;
        //No olvidar inyectar la conexion
        public Login(MrodriguezProgramacionNcapasContext context)
        {
            _context = context;
        }


        public ML.Result Loggin(ML.Login login)
        {
            ML.Result result = new ML.Result();
            try
            {
                var item = _context.LoginDTOs.FromSqlRaw($"UsuarioLogin '{login.Email}', '{login.Password}' ").AsEnumerable()
                    .SingleOrDefault();


                if (item != null)
                {

                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Rol = new ML.Rol();
                    usuario.UserName = item.UserName;
                    usuario.Nombre = item.NombreU;
                    usuario.ApellidoPaterno = item.ApellidoPaterno;
                    usuario.ApellidoMaterno = item.ApellidoMaterno;
                    usuario.Email = item.Email;
                    usuario.Password = item.Password;
                    usuario.Rol.NombreR = item.Rol;

                    result.Object = usuario;
                    result.Correct = true;

                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "User p contraseña no encontrada";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
