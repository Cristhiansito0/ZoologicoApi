using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZoologicoApi.Data;
using ZoologicoApi.Models;

namespace ZoologicoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspeciesController : ControllerBase
    {
        private readonly ZoologicoContext _context;

        public EspeciesController(ZoologicoContext context)
        {
            _context = context;
        }

        // GET: api/Especies
        [HttpGet]
        public async Task<ActionResult<ApiResult>> GetEspecies()
        {
            try
            {
                // Incluimos Animales para la relación (aunque podría ser pesado)
                var data = await _context.Especies
                                         .Include(e => e.Animales)
                                         .ToListAsync();

                // 200 OK
                return Ok(ApiResult<List<Especie>>.SuccessResult(data));
            }
            catch (Exception ex)
            {
                // 500 Internal Server Error
                return StatusCode(500, ApiResult<List<Especie>>.ErrorResult($"Error al obtener especies: {ex.Message}", 500));
            }

        }

        // GET: api/Especies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult>> GetEspecie(int id)
        {
            try
            {
                // Asumo que el ID de la clave primaria se llama Id, no Codigo.
                var especie = await _context
                    .Especies
                    .Include(e => e.Animales)
                    .FirstOrDefaultAsync(e => e.Id == id); // Usamos 'Id'

                if (especie == null)
                {
                    // 404 Not Found
                    return NotFound(ApiResult<Especie>.ErrorResult("Especie no encontrada.", 404));
                }

                // 200 OK
                return Ok(ApiResult<Especie>.SuccessResult(especie));
            }
            catch (Exception ex)
            {
                // 500 Internal Server Error
                return StatusCode(500, ApiResult<Especie>.ErrorResult($"Error al obtener la especie: {ex.Message}", 500));
            }

        }

        // PUT: api/Especies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult>> PutEspecie(int id, Especie especie)
        {
            // Validamos que el ID de la ruta coincida con el ID del cuerpo
            if (id != especie.Id)
            {
                // 400 Bad Request
                return BadRequest(ApiResult<object>.ErrorResult("El ID de la ruta no coincide con el ID de la especie.", 400));
            }

            _context.Entry(especie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EspecieExists(id))
                {
                    // 404 Not Found
                    return NotFound(ApiResult<object>.ErrorResult("Especie no encontrada para actualizar.", 404));
                }
                else
                {
                    // 500 Internal Server Error
                    return StatusCode(500, ApiResult<object>.ErrorResult("Error de concurrencia al actualizar.", 500));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<object>.ErrorResult($"Error al actualizar la especie: {ex.Message}", 500));
            }

            // 204 No Content (Éxito sin devolver datos)
            return NoContent();

        }

        // POST: api/Especies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult>> PostEspecie(Especie especie)
        {
            try
            {
                _context.Especies.Add(especie);
                await _context.SaveChangesAsync();

                // 201 Created
                return CreatedAtAction(nameof(GetEspecie), new { id = especie.Id }, ApiResult<Especie>.SuccessResult(especie, "Especie creada.", 201));
            }
            catch (Exception ex)
            {
                // 500 Internal Server Error (o 400 si es un error de validación)
                return StatusCode(500, ApiResult<Especie>.ErrorResult($"Error al crear la especie: {ex.Message}", 500));
            }

        }

        // DELETE: api/Especies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult>> DeleteEspecie(int id)
        {
            var especie = await _context.Especies.FindAsync(id);
            if (especie == null)
            {
                return NotFound();
            }

            _context.Especies.Remove(especie);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        private bool EspecieExists(int id)
        {
            return _context.Especies.Any(e => e.Id == id);
        }
    }
}
