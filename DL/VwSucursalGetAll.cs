using System;
using System.Collections.Generic;

namespace DL;

public partial class VwSucursalGetAll
{
    public string? Sucursal { get; set; }

    public string? Producto { get; set; }

    public int IdProducto { get; set; }

    public int IdSucursal { get; set; }

    public byte[]? Imagen { get; set; }

    public string? Latitud { get; set; }

    public string? Longitud { get; set; }

    public int? Stock { get; set; }

    public int IdProductoSucursal { get; set; }
}
