using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpacadoraLimonAPI.DTOs
{
    public class ProveedorDTO
    {
        public int IdProveedor { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Direccion { get; set; }

        public decimal? Latitud { get; set; }

        public decimal? Longitud { get; set; }

        public string? Telefono { get; set; }
    }
}
