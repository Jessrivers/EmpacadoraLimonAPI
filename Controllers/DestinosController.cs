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
    [Route("api/Destinos")]
    public class DestinosController : ControllerBase
    {
        private readonly DbEmpacadoraContext db;
        private readonly IMapper mapper;

        public DestinosController(DbEmpacadoraContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<List<DestinoDTO>> Get()
        {
            var destinos = await db.Destinos.OrderBy(x => x.IdDestino).ToListAsync();
            return mapper.Map<List<DestinoDTO>>(destinos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var destino = await db.Destinos.FirstOrDefaultAsync(x => x.IdDestino == id);
            if (destino == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<DestinoDTO>(destino));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DestinoDTOCrear destinoDTO)
        {
            var destino = mapper.Map<Destino>(destinoDTO);
            await db.Destinos.AddAsync(destino);
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] DestinoDTO destinoDTO)
        {
            var existe = await db.Destinos.AnyAsync(x => x.IdDestino == id);
            if (!existe)
            {
                return NotFound();
            }

            var destino = mapper.Map<Destino>(destinoDTO);
            destino.IdDestino = id;
            db.Update(destino);
            await db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existe = await db.Destinos.AnyAsync(x => x.IdDestino == id);
            if (!existe)
            {
                return NotFound();
            }

            db.Remove(new Destino { IdDestino = id });
            await db.SaveChangesAsync();
            return NoContent();
        }
    }
}
