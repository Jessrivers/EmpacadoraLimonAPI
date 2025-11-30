using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpacadoraLimonAPI.DTOs
{
    public class LoteDTOCrear
    {
        public int IdProveedor { get; set; }

        public DateOnly? Fecha { get; set; }

        public decimal? CantidadKg { get; set; }

        public string? Calidad { get; set; }

        public IFormFile? Imagen { get; set; }
    }
}
