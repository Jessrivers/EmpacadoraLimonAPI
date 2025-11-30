using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpacadoraLimonAPI.DTOs
{
    public class MovimientoDTOCrear
    {
        public int IdLote { get; set; }

        public int IdDestino { get; set; }

        public DateOnly? Fecha { get; set; }

        public string? Transporte { get; set; }

        public string? Observaciones { get; set; }
    }
}
