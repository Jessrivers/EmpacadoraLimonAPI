using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmpacadoraLimonAPI.DTOs;
using EmpacadoraLimonAPI.Models;
using EmpacadoraLimonAPI.Services;

namespace EmpacadoraLimonAPI.Controllers
{
    [ApiController]
    [Route("api/Lotes")]
    public class LotesController : ControllerBase
    {
        private readonly DbEmpacadoraContext db;
        private readonly IMapper mapper;
        private readonly IAlmacenamiento almacenador;

        public LotesController(DbEmpacadoraContext db, IMapper mapper, IAlmacenamiento a)
        {
            this.db = db;
            this.mapper = mapper;
            this.almacenador = a;
        }

        [HttpGet]
        public async Task<List<LoteDTO>> Get()
        {
            var lotes = await db.Lotes
                .Include(x => x.IdProveedorNavigation)
                .OrderBy(x => x.IdLote)
                .ToListAsync();
            return mapper.Map<List<LoteDTO>>(lotes);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lote = await db.Lotes
                .Include(x => x.IdProveedorNavigation)
                .FirstOrDefaultAsync(x => x.IdLote == id);

            if (lote == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<LoteDTO>(lote));
        }

        [HttpGet("Proveedor/{idProveedor:int}")]
        public async Task<List<LoteDTO>> GetByProveedor(int idProveedor)
        {
            var lotes = await db.Lotes
                .Include(x => x.IdProveedorNavigation)
                .Where(x => x.IdProveedor == idProveedor)
                .OrderBy(x => x.Fecha)
                .ToListAsync();

            return mapper.Map<List<LoteDTO>>(lotes);
        }

        [HttpGet("Fecha")]
        public async Task<List<LoteDTO>> GetByFecha([FromQuery] DateOnly fechaInicio, [FromQuery] DateOnly fechaFin)
        {
            var lotes = await db.Lotes
                .Include(x => x.IdProveedorNavigation)
                .Where(x => x.Fecha >= fechaInicio && x.Fecha <= fechaFin)
                .OrderBy(x => x.Fecha)
                .ToListAsync();

            return mapper.Map<List<LoteDTO>>(lotes);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] LoteDTOCrear loteDTO)
        {
            var lote = mapper.Map<Lote>(loteDTO);
            string urlImagen = "";
            if (loteDTO.Imagen is not null)
            {
                urlImagen = await almacenador.AlmacenarImagen("Lotes", loteDTO.Imagen);
                lote.UrlImagen = urlImagen;
            }
            await db.Lotes.AddAsync(lote);
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] LoteDTO loteDTO)
        {
            var existe = await db.Lotes.AnyAsync(x => x.IdLote == id);
            if (!existe)
            {
                return NotFound();
            }

            var lote = mapper.Map<Lote>(loteDTO);
            lote.IdLote = id;
            db.Update(lote);
            await db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existe = await db.Lotes.AnyAsync(x => x.IdLote == id);
            if (!existe)
            {
                return NotFound();
            }

            db.Remove(new Lote { IdLote = id });
            await db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("SubirImagen")]
        public async Task<IActionResult> PostImagen([FromForm] Fotografia imagen)
        {
            try
            {
                string urlFoto = "";
                if (imagen.Foto is not null)
                {
                    urlFoto = await almacenador.AlmacenarImagen("Lotes", imagen.Foto);
                }
                return Ok(new { url = urlFoto, nombre = imagen.Nombre });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
