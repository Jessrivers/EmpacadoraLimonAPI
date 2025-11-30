using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmpacadoraLimonAPI.DTOs;
using EmpacadoraLimonAPI.Models;

namespace EmpacadoraLimonAPI.Controllers
{
    [ApiController]
    [Route("api/Movimientos")]
    public class MovimientosController : ControllerBase
    {
        private readonly DbEmpacadoraContext db;
        private readonly IMapper mapper;

        public MovimientosController(DbEmpacadoraContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<List<MovimientoDTO>> Get()
        {
            var movimientos = await db.Movimientos
                .Include(x => x.IdDestinoNavigation)
                .OrderBy(x => x.IdMovimiento)
                .ToListAsync();

            return mapper.Map<List<MovimientoDTO>>(movimientos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movimiento = await db.Movimientos
                .Include(x => x.IdDestinoNavigation)
                .FirstOrDefaultAsync(x => x.IdMovimiento == id);

            if (movimiento == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<MovimientoDTO>(movimiento));
        }

        [HttpGet("Lote/{idLote:int}")]
        public async Task<List<MovimientoDTO>> GetByLote(int idLote)
        {
            var movimientos = await db.Movimientos
                .Include(x => x.IdDestinoNavigation)
                .Where(x => x.IdLote == idLote)
                .OrderBy(x => x.Fecha)
                .ToListAsync();

            return mapper.Map<List<MovimientoDTO>>(movimientos);
        }

        [HttpGet("Ruta/{id:int}")]
        public async Task<IActionResult> GetRutaMapa(int id)
        {
            var movimiento = await db.Movimientos
                .Include(x => x.IdLoteNavigation)
                    .ThenInclude(l => l!.IdProveedorNavigation)
                .Include(x => x.IdDestinoNavigation)
                .FirstOrDefaultAsync(x => x.IdMovimiento == id);

            if (movimiento == null)
            {
                return NotFound();
            }

            var ruta = new RutaMapaDTO
            {
                IdMovimiento = movimiento.IdMovimiento,
                Fecha = movimiento.Fecha,
                Transporte = movimiento.Transporte,
                Proveedor = movimiento.IdLoteNavigation?.IdProveedorNavigation != null
                    ? new ProveedorCoordenadas
                    {
                        Nombre = movimiento.IdLoteNavigation.IdProveedorNavigation.Nombre,
                        Latitud = movimiento.IdLoteNavigation.IdProveedorNavigation.Latitud,
                        Longitud = movimiento.IdLoteNavigation.IdProveedorNavigation.Longitud
                    }
                    : null,
                Destino = movimiento.IdDestinoNavigation != null
                    ? new DestinoCoordenadas
                    {
                        Nombre = movimiento.IdDestinoNavigation.Nombre,
                        Latitud = movimiento.IdDestinoNavigation.Latitud,
                        Longitud = movimiento.IdDestinoNavigation.Longitud
                    }
                    : null
            };

            return Ok(ruta);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovimientoDTOCrear movimientoDTO)
        {
            var movimiento = mapper.Map<Movimiento>(movimientoDTO);
            await db.Movimientos.AddAsync(movimiento);
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] MovimientoDTO movimientoDTO)
        {
            var existe = await db.Movimientos.AnyAsync(x => x.IdMovimiento == id);
            if (!existe)
            {
                return NotFound();
            }

            var movimiento = mapper.Map<Movimiento>(movimientoDTO);
            movimiento.IdMovimiento = id;
            db.Update(movimiento);
            await db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existe = await db.Movimientos.AnyAsync(x => x.IdMovimiento == id);
            if (!existe)
            {
                return NotFound();
            }

            db.Remove(new Movimiento { IdMovimiento = id });
            await db.SaveChangesAsync();
            return NoContent();
        }
    }
}
