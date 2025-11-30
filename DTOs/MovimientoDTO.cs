using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpacadoraLimonAPI.DTOs
{
    public class MovimientoDTO
    {
        public int IdMovimiento { get; set; }

        public int IdLote { get; set; }

        public int IdDestino { get; set; }

        public string? NombreDestino { get; set; }

        public DateOnly? Fecha { get; set; }

        public string? Transporte { get; set; }

        public string? Observaciones { get; set; }
    }
}
