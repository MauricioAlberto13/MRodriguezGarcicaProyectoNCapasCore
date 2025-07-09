using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        private readonly MrodriguezProgramacionNcapasContext _context;
        //No olvidar inyectar la conexion
        public Colonia(MrodriguezProgramacionNcapasContext context)
        {
            _context = context;
        }

        public ML.Result GetColoniaByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                var listColonias = _context.Colonia.FromSqlRaw($"GetColinasByMunicipio2 {IdMunicipio}").AsEnumerable()
                    .ToList();


                if (listColonias.Count > 0)
                {
                    result.Objects = new List<object>();
                    foreach (var item in listColonias)
                    {
                        ML.Colonia colonia = new ML.Colonia();
                        colonia.IdColonia = item.IdColonia;
                        colonia.Nombre = item.Nombre;

                        result.Objects.Add(colonia);
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
