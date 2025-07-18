using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {

        private readonly MrodriguezProgramacionNcapasContext _context;
        //No olvidar inyectar la conexion
        public Producto(MrodriguezProgramacionNcapasContext context)
        {
            _context = context;
        }

        public ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                var foto = new SqlParameter("@Imagen", SqlDbType.VarBinary);
                if (producto.Imagen != null)
                {
                    foto.Value = producto.Imagen;
                }
                else
                {
                    foto.Value = DBNull.Value;
                }

                int FilasAfectadas = _context.Database.ExecuteSql($"ProductoAdd {producto.Nombre}, {producto.Descripcion}, {producto.Precio},  {foto},{producto.SubCategoria.IdSubCategoria}");
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

        public ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                var foto = new SqlParameter("@Imagen", SqlDbType.VarBinary);
                if (producto.Imagen != null)
                {
                    foto.Value = producto.Imagen;
                }
                else
                {
                    foto.Value = DBNull.Value;
                }
                int FilasAfectadas = _context.Database.ExecuteSql($"ProductoUpdate {producto.Nombre}, {producto.Descripcion}, {producto.Precio},  {foto},{producto.SubCategoria.IdSubCategoria} ,{producto.IdProducto}");
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

        public ML.Result Delete(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                //REcuerda que si es una consulta como Add, Update o Delete se usa Database Execute SQL

                int FilasAfectadas = _context.Database.ExecuteSql($"ProductoDelete {IdProducto}");
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

        public ML.Result GetById(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                var item = _context.VwProductosGetAlls.FromSqlRaw($"ProductoGetById {IdProducto}").AsEnumerable()
                    .FirstOrDefault();


                if (item != null)
                {

                    ML.Producto producto= new ML.Producto();
                    //producto.Rol = new ML.Rol();

                    producto.IdProducto= item.IdProducto;
                    producto.Nombre= item.Nombre;
                    producto.Descripcion= item.Descripcion;
                    producto.Precio= item.Precio;
         

                    //////


                    producto.Imagen = item.Imagen;
                    //producto.Rol.IdRol = item.Rol;
                    producto.SubCategoria = new ML.SubCategoria();
                    producto.SubCategoria.Categoria = new ML.Categoria();

                    producto.SubCategoria.Nombre = item.SubCategoria;
                    producto.SubCategoria.Categoria.Nombre = item.Categoria;

                    ///aqui esta el error
                    ///

                    // producto.Direccion.Colonia.IdColonia = (int)item.IdColonia;

                    producto.SubCategoria.IdSubCategoria = item.IdSubCategoria ?? 0;

                    //   producto.Direccion.Colonia.Municipio.IdMunicipio = (int)item.IdMunicipio;

                    producto.SubCategoria.Categoria.IdCategoria = item.IdCategoria ?? 0;




                    //////
                    result.Object = producto;
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

                var query = _context.VwProductosGetAlls.FromSqlRaw("select * from vwProductosGetAlls ").ToList();
                //var query = _context.VwUsuarioGetAlls.FromSqlRaw($"UsuarioGetsAllView '{Usuario.}',' ',' ',' ' ").ToList();
                //var query = _context.VwUsuarioGetAlls.FromSqlRaw($"UsuarioGetsAllView ").ToList();
                //Recuerdad que entity core no mapea los store procedures y se tienen que mandar a llamar con From SQl Raw en caso de que sea una consulta SELECT
                //var listUsers = context.UsuarioGetsAllView(producto.Nombre, producto.ApellidoPaterno, producto.ApellidoMaterno, producto.Rol.IdRol).ToList();

                //' '
                result.Objects = new List<object>();

                if (query.Count > 0)
                {
                    foreach (var item in query)
                    {

                        ML.Producto producto = new ML.Producto();
                        //producto.Rol = new ML.Rol();

                        producto.IdProducto = item.IdProducto;
                        producto.Nombre = item.Nombre;
                        producto.Descripcion = item.Descripcion;
                        producto.Precio = item.Precio;


                        //////


                        producto.Imagen = item.Imagen;
                        //producto.Rol.IdRol = item.Rol;
                        producto.SubCategoria = new ML.SubCategoria();
                        producto.SubCategoria.Categoria = new ML.Categoria();

                        producto.SubCategoria.Nombre = item.SubCategoria;
                        producto.SubCategoria.Categoria.Nombre = item.Categoria;

                        ///aqui esta el error
                        ///

                        // producto.Direccion.Colonia.IdColonia = (int)item.IdColonia;

                        producto.SubCategoria.IdSubCategoria = item.IdSubCategoria ?? 0;

                        //   producto.Direccion.Colonia.Municipio.IdMunicipio = (int)item.IdMunicipio;

                        producto.SubCategoria.Categoria.IdCategoria = item.IdCategoria ?? 0;

                        result.Objects.Add(producto);
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

        public ML.Result GetAllV(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                //Se quita el bloque using ya que la conexion ya se encuentra y solo vive en el BL
                // var query = _context.VwUsuarioGetAlls.FromSqlRaw("select * from vwUsuarioGetAll ").ToList();

                //  var query = _context.VwUsuarioGetAlls.FromSqlRaw("exec UsuarioGetsAllView  '', '', '',0 ").ToList();
                int idSub = (int)((producto.SubCategoria!= null) ? producto.SubCategoria.IdSubCategoria: 0);

                var query = _context.VwProductosGetAlls
                    .FromSqlRaw($"EXEC ProductoGetAll '{idSub}', '{producto.SubCategoria.Categoria.IdCategoria}'")
                    .ToList();
                //var query = _context.VwUsuarioGetAlls.FromSqlRaw($"UsuarioGetsAllView ").ToList();
                //Recuerdad que entity core no mapea los store procedures y se tienen que mandar a llamar con From SQl Raw en caso de que sea una consulta SELECT
                //var listUsers = context.UsuarioGetsAllView(producto.Nombre, producto.ApellidoPaterno, producto.ApellidoMaterno, producto.Rol.IdRol).ToList();

                //' '
                result.Objects = new List<object>();

                if (query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        
                        //producto.Rol = new ML.Rol();

                        producto.IdProducto = item.IdProducto;
                        producto.Nombre = item.Nombre;
                        producto.Descripcion = item.Descripcion;
                        producto.Precio = item.Precio;

                        producto.Imagen = item.Imagen;
                        //producto.Rol.IdRol = item.Rol;
                        producto.SubCategoria = new ML.SubCategoria();
                        producto.SubCategoria.Categoria = new ML.Categoria();

                        producto.SubCategoria.Nombre = item.SubCategoria;
                        producto.SubCategoria.Categoria.Nombre = item.Categoria;

                        ///aqui esta el error
                        ///

                        //// producto.Direccion.Colonia.IdColonia = (int)item.IdColonia;

                        //producto.SubCategoria.IdSubCategoria = item.IdSubCategoria ?? 0;

                        ////   producto.Direccion.Colonia.Municipio.IdMunicipio = (int)item.IdMunicipio;

                        //producto.SubCategoria.Categoria.IdCategoria = item.IdCategoria ?? 0;

                        result.Objects.Add(producto);
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
    }
}
