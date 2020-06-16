using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Exceptions;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _20GRPED.MVC2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EscolaController : ControllerBase
    {
        private readonly IEscolaService _escolaService;

        public EscolaController(
            IEscolaService escolaService)
        {
            _escolaService = escolaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EscolaEntity>>> GetEscolaEntity()
        {
            var escolas = await _escolaService.GetAllAsync();
            return Ok(escolas.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EscolaEntity>> GetEscolaEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var escolaEntity = await _escolaService.GetByIdAsync(id);

            if (escolaEntity == null)
            {
                return NotFound("Not found message test!");
            }

            return escolaEntity;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEscolaEntity(int id, EscolaEntity escolaEntity)
        {
            if (id != escolaEntity.Id)
            {
                return BadRequest();
            }

            try
            {
                await _escolaService.UpdateAsync(escolaEntity);
            }
            catch (RepositoryException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<EscolaEntity>> PostEscolaEntity(EscolaEntity escolaEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _escolaService.InsertAsync(escolaEntity);

            return Ok(escolaEntity);
            //return CreatedAtAction(
            //    "GetEscolaEntity",
            //    new { id = escolaEntity.Id }, escolaEntity);
        }

        // DELETE: api/Escola/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EscolaEntity>> DeleteEscolaEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var escolaEntity = await _escolaService.GetByIdAsync(id);
            if (escolaEntity == null)
            {
                return NotFound();
            }

            await _escolaService.DeleteAsync(id);

            return Ok(escolaEntity);
        }
    }
}
