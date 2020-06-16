using System;
using System.Collections.Generic;
using System.Linq;
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
    public class professorController : ControllerBase
    {
        private readonly IProfessorService _professorService;

        public professorController(
            IProfessorService professorService)
        {
            _professorService = professorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfessorEntity>>> GetprofessorEntity()
        {
            var professors = await _professorService.GetAllAsync();
            return Ok(professors.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfessorEntity>> GetprofessorEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var professorEntity = await _professorService.GetByIdAsync(id);

            if (professorEntity == null)
            {
                return NotFound();
            }

            return Ok(professorEntity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutprofessorEntity(int id, ProfessorEntity professorEntity)
        {
            if (id != professorEntity.Id)
            {
                return BadRequest();
            }

            try
            {
                await _professorService.UpdateAsync(professorEntity);
                return Ok();
            }
            catch (EntityValidationException e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }
            catch (RepositoryException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ProfessorEntity>> PostprofessorEntity(ProfessorEntity professorEntity)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            try
            {
                await _professorService.InsertAsync(professorEntity);

                return Ok(professorEntity);
            }
            catch (EntityValidationException e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/professor/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProfessorEntity>> DeleteprofessorEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var professorEntity = await _professorService.GetByIdAsync(id);
            if (professorEntity == null)
            {
                return NotFound();
            }

            await _professorService.DeleteAsync(id);

            return Ok(professorEntity);
        }
    }
}
