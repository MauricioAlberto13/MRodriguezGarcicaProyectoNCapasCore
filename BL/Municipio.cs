using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {


        private readonly MrodriguezProgramacionNcapasContext _context;
        //No olvidar inyectar la conexion
        public Municipio(MrodriguezProgramacionNcapasContext context)
        {
            _context = context;
        }


        public ML.Result GetIdMunicipio(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                var listColonias = _context.VwUsuarioGetAlls.FromSqlRaw($"GetMunicipioByIdEstado {IdEstado}").AsEnumerable()
                    .ToList();


                if (listColonias.Count > 0)
                {
                    result.Objects = new List<object>();
                    foreach (var item in listColonias)
                    {
                        ML.Municipio municipio = new ML.Municipio();
                        // municipio.Estado = new ML.Estado();

                        municipio.IdMunicipio = item.IdMunicipio;
                        municipio.NombreMunicipio = item.Municipio;
                        //municipio.Estado.NombreEstado = item.Estado;

                        result.Objects.Add(municipio);
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
