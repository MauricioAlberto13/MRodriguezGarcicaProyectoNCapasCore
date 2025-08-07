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
    public class ProductoSucursal
    {

        private readonly MrodriguezProgramacionNcapasContext _context;
        //No olvidar inyectar la conexion
        public ProductoSucursal(MrodriguezProgramacionNcapasContext context)
        {
            _context = context;
        }

        public ML.Result Add(ML.ProductoSucursal productoSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {

                int FilasAfectadas = _context.Database.ExecuteSql($"ProductoAdd {productoSucursal.Producto.IdProducto}, {productoSucursal.Sucursal.IdSucursal}, {productoSucursal.Stock}");
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

        public ML.Result Update(ML.ProductoSucursal productoSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {

                int FilasAfectadas = _context.Database.ExecuteSql($"ProductoSucursalUpdate {productoSucursal.IdProductoSucursal},{productoSucursal.Stock}");
                if (FilasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Ocurrio un error al actualizar con SP";
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



        public ML.Result Delete(ML.ProductoSucursal productoSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {

                int FilasAfectadas = _context.Database.ExecuteSql($"ProductoSucursalDelete2 {productoSucursal.Producto.IdProducto}");
                if (FilasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Ocurrio un error al actualizar el stock";
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

        public ML.Result Delete2(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {

                int FilasAfectadas = _context.Database.ExecuteSql($"ProductoSucursalDelete2 {IdProducto}");
                if (FilasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Ocurrio un error al actualizar el stock";
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
        //public ML.Result Delete(int IdProducto)
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        //REcuerda que si es una consulta como Add, Update o Delete se usa Database Execute SQL

        //        int FilasAfectadas = _context.Database.ExecuteSql($"ProductoDelete {IdProducto}");
        //        if (FilasAfectadas > 0)
        //        {
        //            result.Correct = true;
        //        }
        //        else
        //        {
        //            result.Correct = false;
        //            result.ErrorMessage = "Ocurrio un error al eliminar desde EF con SP";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }
        //    return result;
        //}

        public ML.Result GetById(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                var item = _context.VwProductosGetAlls.FromSqlRaw($"ProductoGetById {IdProducto}").AsEnumerable()
                    .FirstOrDefault();


                if (item != null)
                {

                    ML.Producto producto = new ML.Producto();
                    //producto.Rol = new ML.Rol();

                    producto.IdProducto = (int)item.IdProducto;
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

                var query = _context.VwSucursalGetAlls.FromSqlRaw("select * from vwSucursalGetAlls").ToList();
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

                        ML.ProductoSucursal p = new ML.ProductoSucursal();

                        p.Sucursal = new ML.Sucursal();
                        p.Producto = new ML.Producto();


                        p.Producto.Nombre = item.Producto;
                        p.IdProductoSucursal = item.IdProductoSucursal;
                        p.Producto.IdProducto = (int)item.IdProducto;
                        p.Sucursal.Nombre = item.Sucursal;
                        p.Sucursal.IdSucursal = (int)item.IdSucursal;
                        p.Stock = item.Stock;
                        p.Sucursal.Latitud = item.Latitud;
                        p.Sucursal.Longitud = item.Longitud;
                        p.Producto.Imagen = item.Imagen;


                        result.Objects.Add(p);
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

        public ML.Result GetAllV(ML.ProductoSucursal productoSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                //Se quita el bloque using ya que la conexion ya se encuentra y solo vive en el BL
                // var query = _context.VwUsuarioGetAlls.FromSqlRaw("select * from vwUsuarioGetAll ").ToList();

                //  var query = _context.VwUsuarioGetAlls.FromSqlRaw("exec UsuarioGetsAllView  '', '', '',0 ").ToList();
                //int idPro = (int)((productoSucursal.Producto != null) ? productoSucursal.Producto.IdProducto : 0);
                int idSuc = (int)((productoSucursal.Sucursal != null) ? productoSucursal.Sucursal.IdSucursal : 0);

                var query = _context.VwSucursalGetAlls
                    .FromSqlRaw($"EXEC ProductoSucursalById '{idSuc}'")
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
                        ML.ProductoSucursal p = new ML.ProductoSucursal();

                        p.Sucursal = new ML.Sucursal();
                        p.Producto = new ML.Producto();

                        p.IdProductoSucursal = item.IdProductoSucursal;
                        p.Producto.Nombre = item.Producto;
                        p.Producto.IdProducto = (int)item.IdProducto;
                        p.Sucursal.Nombre= item.Sucursal;
                        p.Sucursal.IdSucursal= (int)item.IdSucursal;
                        p.Stock= item.Stock;
                        p.Sucursal.Latitud= item.Latitud;
                        p.Sucursal.Longitud= item.Longitud;
                        p.Producto.Imagen = item.Imagen;
                        //p.IdProductoSucursal = (int)item.Sucursal;
                        //p.Nombre = item.Nombre;

                        ///aqui esta el error
                        ///

                        //// producto.Direccion.Colonia.IdColonia = (int)item.IdColonia;

                        //producto.SubCategoria.IdSubCategoria = item.IdSubCategoria ?? 0;

                        ////   producto.Direccion.Colonia.Municipio.IdMunicipio = (int)item.IdMunicipio;

                        //producto.SubCategoria.Categoria.IdCategoria = item.IdCategoria ?? 0;

                        result.Objects.Add(p);
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
