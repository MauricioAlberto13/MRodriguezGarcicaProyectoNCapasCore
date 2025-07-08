using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        private readonly MrodriguezProgramacionNcapasContext _context;
        //No olvidar inyectar la conexion
        public Estado(MrodriguezProgramacionNcapasContext context)
        {
            _context = context;
        }
        public ML.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                //Se quita el bloque using ya que la conexion ya se encuentra y solo vive en el BL

                var query = _context.Estados.FromSqlRaw("EstadoGetAll").ToList();
                //Recuerdad que entity core no mapea los store procedures y se tienen que mandar a llamar con From SQl Raw en caso de que sea una consulta SELECT
                //var listUsers = context.UsuarioGetsAllView(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Rol.IdRol).ToList();


                result.Objects = new List<object>();

                if (query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        ML.Estado estado = new ML.Estado();
                        estado.IdEstado = item.IdEstado;
                        estado.NombreEstado = item.Nombre;


                        result.Objects.Add(estado);
                    }
                    result.Correct = true;
                }
                else
                {
                    result.ErrorMessage = "Sin resultados";
                    result.Correct = false;
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
