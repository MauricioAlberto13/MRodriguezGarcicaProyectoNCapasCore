using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public class Usuario
    {


        private readonly MrodriguezProgramacionNcapasContext _context;
        //No olvidar inyectar la conexion
        public Usuario(MrodriguezProgramacionNcapasContext context)
        {
            _context = context;
        }

        public ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                var foto = new SqlParameter("@Imagen", SqlDbType.VarBinary);
                if (usuario.Imagen != null)
                {
                    foto.Value = usuario.Imagen;
                }
                else
                {
                    foto.Value = DBNull.Value;
                }
               
                int FilasAfectadas = _context.Database.ExecuteSql($"UsuarioAdd {usuario.UserName}, {usuario.Nombre}, {usuario.ApellidoPaterno}, {usuario.ApellidoMaterno}, {usuario.Email}, {usuario.Password}, {usuario.Sexo}, {usuario.Telefono}, {usuario.Celular}, {usuario.Fecha}, {usuario.CURP}, {usuario.Rol.IdRol}, {foto}, {usuario.Direccion.Calle}, {usuario.Direccion.NumeroInterior}, {usuario.Direccion.NumeroExterior}, {usuario.Direccion.Colonia.IdColonia}");
                if (FilasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Ocurrio un error al insertar con SP";
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

        public ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                var foto = new SqlParameter("@Imagen", SqlDbType.VarBinary);
                if (usuario.Imagen != null)
                {
                    foto.Value = usuario.Imagen;
                }
                else
                {
                    foto.Value = DBNull.Value;
                }
                int FilasAfectadas = _context.Database.ExecuteSql($"UsuarioUpdate {usuario.UserName}, {usuario.Nombre}, {usuario.ApellidoPaterno}, {usuario.ApellidoMaterno}, {usuario.Email}, {usuario.Password}, {usuario.Sexo}, {usuario.Telefono}, {usuario.Celular}, {usuario.Fecha}, {usuario.CURP}, {usuario.Rol.IdRol}, {foto},{usuario.IdUsuario}, {usuario.Direccion.Calle}, {usuario.Direccion.NumeroInterior}, {usuario.Direccion.NumeroExterior}, {usuario.Direccion.Colonia.IdColonia}");
                if (FilasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Ocurrio un error al insertar con SP";
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

        public ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                //REcuerda que si es una consulta como Add, Update o Delete se usa Database Execute SQL

                int FilasAfectadas = _context.Database.ExecuteSql($"UsuarioDelete {IdUsuario}");
                if (FilasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Ocurrio un error al eliminar desde EF con SP";
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

        public ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                var item = _context.VwUsuarioGetAlls.FromSqlRaw($"UsuarioGetById2 {IdUsuario}").AsEnumerable()
                    .FirstOrDefault();


                if (item != null)
                {

                    ML.Usuario usuario = new ML.Usuario();
                    //usuario.Rol = new ML.Rol();

                    usuario.IdUsuario = item.IdUsuario;
                    usuario.UserName = item.UserName;
                    usuario.Nombre = item.Nombre;
                    usuario.ApellidoPaterno = item.ApellidoPaterno;
                    usuario.ApellidoMaterno = item.ApellidoMaterno;
                    usuario.Email = item.Email;
                    usuario.Password = item.Password;
                    usuario.Sexo = item.Sexo;
                    usuario.Telefono = item.Telefono;
                    usuario.Celular = item.Celular;
                    usuario.Fecha = item.FechaNacimiento;
                    usuario.CURP = item.Curp;
                    //////
                    ///
                    ////////////
                    usuario.Rol = new ML.Rol();
                    ///////////
                    usuario.Rol.IdRol = (int)item.IdRol;


                    usuario.Imagen = item.Imagen;
                    //usuario.Rol.IdRol = item.Rol;
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                    usuario.Direccion.Calle = item.Calle;
                    usuario.Direccion.NumeroExterior = item.NumeroExterior;
                    usuario.Direccion.NumeroInterior = item.NumeroInterior;

                    ///aqui esta el error
                    ///

                    // usuario.Direccion.Colonia.IdColonia = (int)item.IdColonia;

                    usuario.Direccion.Colonia.IdColonia = item.IdColonia ?? 0;

                    //   usuario.Direccion.Colonia.Municipio.IdMunicipio = (int)item.IdMunicipio;

                    usuario.Direccion.Colonia.Municipio.IdMunicipio = item.IdMunicipio ?? 0;


                    //  usuario.Direccion.Colonia.Municipio.Estado.IdEstado = (int)item.IdEstado;

                    usuario.Direccion.Colonia.Municipio.Estado.IdEstado = item.IdEstado ?? 0;

                    //////
                    result.Object = usuario;
                    result.Correct = true;

                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Usuario no encontrado en el SP de Ef";
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

        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                //Se quita el bloque using ya que la conexion ya se encuentra y solo vive en el BL

               var query = _context.VwUsuarioGetAlls.FromSqlRaw("select * from vwUsuarioGetAll ").ToList();
                //var query = _context.VwUsuarioGetAlls.FromSqlRaw($"UsuarioGetsAllView '{Usuario.}',' ',' ',' ' ").ToList();
                //var query = _context.VwUsuarioGetAlls.FromSqlRaw($"UsuarioGetsAllView ").ToList();
                //Recuerdad que entity core no mapea los store procedures y se tienen que mandar a llamar con From SQl Raw en caso de que sea una consulta SELECT
                //var listUsers = context.UsuarioGetsAllView(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Rol.IdRol).ToList();

                //' '
                result.Objects = new List<object>();

                if (query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        ML.Usuario usuarioItem = new ML.Usuario();
                        usuarioItem.Rol = new ML.Rol();
                        usuarioItem.Direccion = new ML.Direccion();
                        usuarioItem.Direccion.Colonia = new ML.Colonia();
                        usuarioItem.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuarioItem.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        usuarioItem.IdUsuario = item.IdUsuario;
                        usuarioItem.UserName = item.UserName;
                        usuarioItem.Nombre = item.Nombre;
                        usuarioItem.ApellidoPaterno = item.ApellidoPaterno;
                        usuarioItem.ApellidoMaterno = item.ApellidoMaterno;
                        usuarioItem.Email = item.Email;
                        usuarioItem.Password = item.Password;
                        usuarioItem.Sexo = item.Sexo;
                        usuarioItem.Telefono = item.Telefono;
                        usuarioItem.Celular = item.Celular;
                        usuarioItem.Fecha = item.FechaNacimiento;
                        usuarioItem.CURP = item.Curp;

                        usuarioItem.Rol.NombreR = item.Rol;

                        usuarioItem.Imagen = item.Imagen;
                        //   usuarioItem.ImagenBase64 = Convert.ToBase64String(item.Imagen);


                        usuarioItem.Direccion.Calle = item.Calle;
                        usuarioItem.Direccion.NumeroExterior = item.NumeroExterior;
                        usuarioItem.Direccion.NumeroInterior = item.NumeroInterior;
                        usuarioItem.Direccion.Colonia.Nombre = item.Colonia;
                        usuarioItem.Direccion.Colonia.CodigoPostal = item.CodigoPostal;
                        usuarioItem.Direccion.Colonia.Municipio.NombreMunicipio = item.Municipio;
                        usuarioItem.Direccion.Colonia.Municipio.Estado.NombreEstado = item.Estado;

                        usuarioItem.Status = Convert.ToBoolean(item.Status);

                        result.Objects.Add(usuarioItem);
                    }
                    result.Correct = true;
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

        public ML.Result GetAllV(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                //Se quita el bloque using ya que la conexion ya se encuentra y solo vive en el BL
                // var query = _context.VwUsuarioGetAlls.FromSqlRaw("select * from vwUsuarioGetAll ").ToList();

                //  var query = _context.VwUsuarioGetAlls.FromSqlRaw("exec UsuarioGetsAllView  '', '', '',0 ").ToList();
                int idRol = (int)((usuario.Rol != null) ? usuario.Rol.IdRol : 0);

                var query = _context.VwUsuarioGetAlls
                    .FromSqlRaw($"EXEC UsuarioGetsAllView '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', {idRol}")
                    .ToList();
                //var query = _context.VwUsuarioGetAlls.FromSqlRaw($"UsuarioGetsAllView ").ToList();
                //Recuerdad que entity core no mapea los store procedures y se tienen que mandar a llamar con From SQl Raw en caso de que sea una consulta SELECT
                //var listUsers = context.UsuarioGetsAllView(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Rol.IdRol).ToList();

                //' '
                result.Objects = new List<object>();

                if (query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        ML.Usuario usuarioItem = new ML.Usuario();
                        usuarioItem.Rol = new ML.Rol();
                        usuarioItem.Direccion = new ML.Direccion();
                        usuarioItem.Direccion.Colonia = new ML.Colonia();
                        usuarioItem.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuarioItem.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        usuarioItem.IdUsuario = item.IdUsuario;
                        usuarioItem.UserName = item.UserName;
                        usuarioItem.Nombre = item.Nombre;
                        usuarioItem.ApellidoPaterno = item.ApellidoPaterno;
                        usuarioItem.ApellidoMaterno = item.ApellidoMaterno;
                        usuarioItem.Email = item.Email;
                        usuarioItem.Password = item.Password;
                        usuarioItem.Sexo = item.Sexo;
                        usuarioItem.Telefono = item.Telefono;
                        usuarioItem.Celular = item.Celular;
                        usuarioItem.Fecha = item.FechaNacimiento;
                        usuarioItem.CURP = item.Curp;

                        usuarioItem.Rol.NombreR = item.Rol;

                        usuarioItem.Imagen = item.Imagen;
                        //   usuarioItem.ImagenBase64 = Convert.ToBase64String(item.Imagen);


                        usuarioItem.Direccion.Calle = item.Calle;
                        usuarioItem.Direccion.NumeroExterior = item.NumeroExterior;
                        usuarioItem.Direccion.NumeroInterior = item.NumeroInterior;
                        usuarioItem.Direccion.Colonia.Nombre = item.Colonia;
                        usuarioItem.Direccion.Colonia.IdColonia = item.IdColonia;
                        usuarioItem.Direccion.Colonia.CodigoPostal = item.CodigoPostal;
                        usuarioItem.Direccion.Colonia.Municipio.NombreMunicipio = item.Municipio;
                        usuarioItem.Direccion.Colonia.Municipio.Estado.NombreEstado = item.Estado;

                        usuarioItem.Status = Convert.ToBoolean(item.Status);

                        result.Objects.Add(usuarioItem);
                    }
                    result.Correct = true;
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


        public  ML.Result CambiarStatus(int IdUsuario, bool Status)
        {
            ML.Result result = new ML.Result();
            try
            {
               

                int filaAfectada = _context.Database.ExecuteSql($"ChangeStatus {IdUsuario}, {Status}");

                if (filaAfectada > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al cambiar el estado";
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
