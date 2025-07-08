using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        public string? NombreR { get; set; }

        public List<object>? Roles { get; set; }
    }
}
