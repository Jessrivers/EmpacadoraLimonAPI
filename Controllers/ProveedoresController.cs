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
    [Route("api/Proveedores")]
    public class ProveedoresController : ControllerBase
    {
        private readonly DbEmpacadoraContext db;
        private readonly IMapper mapper;

        public ProveedoresController(DbEmpacadoraContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<List<ProveedorDTO>> Get()
        {
            var proveedores = await db.Proveedores.OrderBy(x => x.IdProveedor).ToListAsync();
            return mapper.Map<List<ProveedorDTO>>(proveedores);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var proveedor = await db.Proveedores.FirstOrDefaultAsync(x => x.IdProveedor == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ProveedorDTO>(proveedor));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProveedorDTOCrear proveedorDTO)
        {
            var proveedor = mapper.Map<Proveedor>(proveedorDTO);
            await db.Proveedores.AddAsync(proveedor);
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProveedorDTO proveedorDTO)
        {
            var existe = await db.Proveedores.AnyAsync(x => x.IdProveedor == id);
            if (!existe)
            {
                return NotFound();
            }

            var proveedor = mapper.Map<Proveedor>(proveedorDTO);
            proveedor.IdProveedor = id;
            db.Update(proveedor);
            await db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existe = await db.Proveedores.AnyAsync(x => x.IdProveedor == id);
            if (!existe)
            {
                return NotFound();
            }

            db.Remove(new Proveedor { IdProveedor = id });
            await db.SaveChangesAsync();
            return NoContent();
        }
    }
}
