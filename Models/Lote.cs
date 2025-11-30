using System;
using System.Collections.Generic;

namespace EmpacadoraLimonAPI.Models;

public partial class Lote
{
    public int IdLote { get; set; }

    public int IdProveedor { get; set; }

    public DateTime? FechaRecepcion { get; set; }

    public decimal CantidadKg { get; set; }

    public string? Calidad { get; set; }

    public string? UrlImagen { get; set; }

    public virtual Proveedor? IdProveedorNavigation { get; set; }

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
