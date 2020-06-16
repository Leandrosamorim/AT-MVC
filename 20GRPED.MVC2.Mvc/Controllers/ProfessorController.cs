using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Exceptions;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using _20GRPED.MVC2.Mvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _20GRPED.MVC2.Mvc.Controllers
{
    [Authorize]
    public class ProfessorController : Controller
    {
        private readonly IProfessorService _professorService;
        private readonly IEscolaService _escolaService;

        public ProfessorController(
            IProfessorService professorService,
            IEscolaService escolaService)
        {
            _professorService = professorService;
            _escolaService = escolaService;
        }

        // GET: Livro
        public async Task<IActionResult> Index()
        {
            var professors = await _professorService.GetAllAsync();

            if(professors == null)
                return Redirect("/Identity/Account/Login");

            return View(professors);
        }

        // GET: Livro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professorModel = await _professorService.GetByIdAsync(id.Value);
            if (professorModel == null)
            {
                return NotFound();
            }

            return View(professorModel);
        }

        // GET: Livro/Create
        public async Task<IActionResult> Create()
        {
            var professorViewModel = new ProfessorViewModel(await _escolaService.GetAllAsync());

            return View(professorViewModel);
        }

        // POST: Livro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProfessorEntity professorEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _professorService.InsertAsync(professorEntity);
                    return RedirectToAction(nameof(Index));
                }
                catch (EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                }
            }
            return View(new ProfessorViewModel(professorEntity));
        }

        // GET: Livro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professorModel = await _professorService.GetByIdAsync(id.Value);
            if (professorModel == null)
            {
                return NotFound();
            }

            var professorViewModel = new ProfessorViewModel(professorModel, await _escolaService.GetAllAsync());

            return View(professorViewModel);
        }

        // POST: Livro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProfessorEntity professorEntity)
        {
            if (id != professorEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _professorService.UpdateAsync(professorEntity);
                }
                catch (EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                    return View(new ProfessorViewModel(professorEntity));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _professorService.GetByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(new ProfessorViewModel(professorEntity));
        }

        // GET: Livro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professorModel = await _professorService.GetByIdAsync(id.Value);
            if (professorModel == null)
            {
                return NotFound();
            }

            return View(professorModel);
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _professorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
