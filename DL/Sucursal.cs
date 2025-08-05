using System;
using System.Collections.Generic;

namespace DL;

public partial class Sucursal
{
    public int IdSucursal { get; set; }

    public string? Nombre { get; set; }

    public string? Latitud { get; set; }

    public string? Longitud { get; set; }

    public virtual ICollection<ProductoSucursal> ProductoSucursals { get; set; } = new List<ProductoSucursal>();
}
