using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {

        public int IdDireccion { get; set; }
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [Required(ErrorMessage = "Ingresa una calle es requerido")]

        public string? Calle { get; set; }
        [StringLength(5, ErrorMessage = "Máximo 5 numeros")]
        [Required(ErrorMessage = "Ingresa un NumeroInterior es obligatorio")]
        [RegularExpression("[0-9]", ErrorMessage = "EL Numero Interior no puede llevar letras")]

        public string? NumeroInterior { get; set; }
        [StringLength(5, ErrorMessage = "Máximo 5 numeros")]

        [RegularExpression("[0-9]", ErrorMessage = "EL Numero Interior no puede llevar letras")]
        public string? NumeroExterior { get; set; }

        public ML.Colonia? Colonia { get; set; }

        public List<object>? Direccciones { get; set; }

    }
}
