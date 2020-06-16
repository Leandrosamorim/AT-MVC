using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _20GRPED.MVC2.Data.Context;
using _20GRPED.MVC2.Domain.Model.Entities;

namespace _20GRPED.MVC2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscolaEntitiesController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public EscolaEntitiesController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: api/EscolaEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EscolaEntity>>> GetEscolaes()
        {
            return await _context.Escolas.ToListAsync();
        }

        // GET: api/EscolaEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EscolaEntity>> GetEscolaEntity(int id)
        {
            var escolaEntity = await _context.Escolas
                .Include(x => x.Professores)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (escolaEntity == null)
            {
                return NotFound();
            }

            return escolaEntity;
        }

        // PUT: api/EscolaEntities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEscolaEntity(int id, EscolaEntity escolaEntity)
        {
            if (id != escolaEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(escolaEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EscolaEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EscolaEntities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EscolaEntity>> PostEscolaEntity(EscolaEntity escolaEntity)
        {
            _context.Escolas.Add(escolaEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEscolaEntity", new { id = escolaEntity.Id }, escolaEntity);
        }

        // DELETE: api/EscolaEntities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EscolaEntity>> DeleteEscolaEntity(int id)
        {
            var escolaEntity = await _context.Escolas.FindAsync(id);
            if (escolaEntity == null)
            {
                return NotFound();
            }

            _context.Escolas.Remove(escolaEntity);
            await _context.SaveChangesAsync();

            return escolaEntity;
        }

        private bool EscolaEntityExists(int id)
        {
            return _context.Escolas.Any(e => e.Id == id);
        }
    }
}
