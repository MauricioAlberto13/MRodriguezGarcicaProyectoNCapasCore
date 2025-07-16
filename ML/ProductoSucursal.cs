using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public  class ProductoSucursal
    {
        public int IdProductoSucursal { get; set; }
        public ML.Producto? Producto { get; set; }
        public ML.Producto? Sucursal { get; set; }
        public int Stock{ get; set; }

    }
}
