using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpacadoraLimonAPI.DTOs
{
    public class DestinoDTOCrear
    {
        public string Nombre { get; set; } = null!;

        public string? Direccion { get; set; }

        public decimal? Latitud { get; set; }

        public decimal? Longitud { get; set; }
    }
}
