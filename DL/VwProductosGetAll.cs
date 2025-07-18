using System;
using System.Collections.Generic;

namespace DL;

public partial class VwProductosGetAll
{
    public int IdProducto { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public byte[]? Imagen { get; set; }

    public string? Categoria { get; set; }

    public string? SubCategoria { get; set; }

    public int? IdCategoria { get; set; }

    public int? IdSubCategoria { get; set; }
}
