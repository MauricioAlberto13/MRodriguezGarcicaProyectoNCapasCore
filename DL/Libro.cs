using System;
using System.Collections.Generic;

namespace DL;

public partial class Libro
{
    public int IdLibro { get; set; }

    public string? Titulo { get; set; }

    public int? NumeroPaginas { get; set; }

    public string? Autor { get; set; }

    public string? Editorial { get; set; }

    public DateOnly? FechaPublicacion { get; set; }
}
