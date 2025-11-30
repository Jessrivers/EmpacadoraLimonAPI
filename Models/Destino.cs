using System;
using System.Collections.Generic;

namespace EmpacadoraLimonAPI.Models;

public partial class Destino
{
    public int IdDestino { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public decimal Latitud { get; set; }

    public decimal Longitud { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
