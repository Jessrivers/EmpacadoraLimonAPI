using System;
using System.Collections.Generic;

namespace EmpacadoraLimonAPI.Models;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public decimal Latitud { get; set; }

    public decimal Longitud { get; set; }

    public string? Telefono { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Lote> Lotes { get; set; } = new List<Lote>();
}
