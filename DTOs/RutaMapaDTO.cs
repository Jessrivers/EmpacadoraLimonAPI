using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpacadoraLimonAPI.DTOs
{
    public class RutaMapaDTO
    {
        public int IdMovimiento { get; set; }

        public ProveedorCoordenadas? Proveedor { get; set; }

        public DestinoCoordenadas? Destino { get; set; }

        public DateTime? FechaEnvio { get; set; }

        public string? Transporte { get; set; }
    }

    public class ProveedorCoordenadas
    {
        public string Nombre { get; set; } = null!;
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
    }

    public class DestinoCoordenadas
    {
        public string Nombre { get; set; } = null!;
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
    }
}
