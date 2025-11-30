using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpacadoraLimonAPI.Models
{
    public class Fotografia
    {
        public string? Nombre { get; set; }
        public IFormFile? Foto { get; set; }
    }
}
