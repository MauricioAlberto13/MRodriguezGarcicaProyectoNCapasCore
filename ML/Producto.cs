using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ML
{
   public class Producto
    {
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "Ingresa un nombre  es requerido")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\\s]+$", ErrorMessage = "EL nombre no puede numeros")]

        public string? Nombre { get; set; }
        [StringLength(250, ErrorMessage = "Máximo 250 caracteres")]
        public string? Descripcion { get; set; }

        [RegularExpression("^[0-9.]+$", ErrorMessage = "EL precio no puede llevar letras")]
        public decimal? Precio { get; set; }
        public ML.SubCategoria? SubCategoria { get; set; }
        public Byte[]? Imagen { get; set; }
        public string? ImagenBase64 { get; set; }
        public List<object>? Productos { get; set; }


    }
}
