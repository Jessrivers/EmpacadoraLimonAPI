using System;
using System.Collections.Generic;

namespace EmpacadoraLimonAPI.Models;

public partial class Movimiento
{
    public int IdMovimiento { get; set; }

    public int IdLote { get; set; }

    public int IdDestino { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Transporte { get; set; }

    public string? Observaciones { get; set; }

    public virtual Destino? IdDestinoNavigation { get; set; }

    public virtual Lote? IdLoteNavigation { get; set; }
}
