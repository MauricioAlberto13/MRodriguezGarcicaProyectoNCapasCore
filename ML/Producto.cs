using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ML
{
   public class Producto
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public ML.SubCategoria? SubCategoria { get; set; }
        public Byte[]? Imagen { get; set; }
        public string? ImagenBase64 { get; set; }
        public List<object>? Productos { get; set; }


    }
}
