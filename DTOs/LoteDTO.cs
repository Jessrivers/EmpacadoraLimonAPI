using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpacadoraLimonAPI.DTOs
{
    public class LoteDTO
    {
        public int IdLote { get; set; }

        public int IdProveedor { get; set; }

        public DateOnly? Fecha { get; set; }

        public string? NombreProveedor { get; set; }

        public decimal? CantidadKg { get; set; }

        public string? Calidad { get; set; }

        public string? UrlImagen { get; set; }
    }
}
