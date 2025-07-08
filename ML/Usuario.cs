using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Ingresa un nombre de usuario es requerido")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string?  UserName { get; set; }
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [Required(ErrorMessage = "Ingresa un nombre es requerido")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\\s]+$", ErrorMessage = "EL nombre no puede llevar caracteres o numeros")]

        public string? Nombre { get; set; }

        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [Required(ErrorMessage = "Ingresa un apellido es requerido")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\\s]+$", ErrorMessage = "EL apellido no puede llevar caracteres o numeros")]

        public string? ApellidoPaterno { get; set; }
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\\s]+$", ErrorMessage = "EL apellido no puede llevar caracteres o numeros")]

        public string? ApellidoMaterno { get; set; }

        [EmailAddress(ErrorMessage = "Ingresa un email valido")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Ingresa un password debe ser obligatorio")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]

        public string? Password { get; set; }
        [Required]
        public string? Sexo { get; set; }
        [StringLength(10, ErrorMessage = "Máximo 10 numeros")]
        [Required(ErrorMessage = "Ingresa un telefono es obligatorio")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "EL numero no tiene los 10 digitos y no puede llevar letras")]

        public string? Telefono { get; set; }
        [StringLength(10, ErrorMessage = "Máximo 10 numeros")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "EL numero no tiene los 10 digitos y no puede llevar letras")]

        public string? Celular { get; set; }


        public DateTime? FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Ingresa una fecha es obligatorio")]
        public string? Fecha { get; set; }
        [StringLength(18, MinimumLength = 18, ErrorMessage = "El CURP debe tener 18 caracteres")]
        [Required(ErrorMessage = "Ingresa una CURP es obligatorio")]
        [RegularExpression("^[A-Z]{4}\\d{6}[HM](AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[0-9A-Z]{1}\\d{1}$", ErrorMessage = "La CURP no es valida.")]


        public string? CURP { get; set; }

        //public int IdRol { get; set; }
        //Recuerda hacer el sp de el get all y get all by id
        //  no olvidar isntanciar al modelo de rol y mostrar el npombre del rol no el id

        //public ML.Rol IdRol{ get; set; }

        public bool Status { get; set; }

        public ML.Rol? Rol { get; set; }

        public int IdDireccion { get; set; }
        public ML.Direccion? Direccion { get; set; }
        public List<object>? Usuarios { get; set; }
        public Byte[]? Imagen { get; set; }
        public string? ImagenBase64 { get; set; }
    }
}
