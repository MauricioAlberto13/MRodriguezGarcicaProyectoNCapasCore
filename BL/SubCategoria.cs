using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SubCategoria
    {
        private readonly MrodriguezProgramacionNcapasContext _context;
        //No olvidar inyectar la conexion
        public SubCategoria(MrodriguezProgramacionNcapasContext context)
        {
            _context = context;
        }


        public ML.Result GetSubCategoriaByIdCategoria(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                var query = _context.SubCategoria.FromSqlRaw($"Exec GetSubCategoriaByIdCategoria {IdEstado}").AsEnumerable()
                    .ToList();


                if (query.Count > 0)
                {
                    result.Objects = new List<object>();
                    foreach (var item in query)
                    {
                        ML.SubCategoria subCategoria = new ML.SubCategoria();
                        // municipio.Estado = new ML.Estado();

                        subCategoria.IdSubCategoria = item.IdSubCategoria;
                        //subCategoria.Nombre= item.Nombre;
                        subCategoria.Categoria.Nombre= item.Nombre;
                        // municipio.Estado.NombreEstado = item.n;

                        result.Objects.Add(subCategoria);
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
