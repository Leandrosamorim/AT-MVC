using System;
using _20GRPED.MVC2.Data.Context;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Exceptions;
using _20GRPED.MVC2.Domain.Model.Options;
using Microsoft.Extensions.Options;

namespace _20GRPED.MVC2.Data.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly BibliotecaContext _context;
        private readonly IOptionsMonitor<TestOption> _testOption;

        public ProfessorRepository(
            BibliotecaContext context,
            IOptionsMonitor<TestOption> testOption)
        {
            _context = context;
            _testOption = testOption;
        }

        public async Task DeleteAsync(int id)
        {
            var livroModel = await _context.Professores.FindAsync(id);
            _context.Professores.Remove(livroModel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProfessorEntity>> GetAllAsync()
        {
            return await _context.Professores.ToListAsync();
        }

        public async Task<ProfessorEntity> GetByIdAsync(int id)
        {
            return await _context.Professores.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(ProfessorEntity insertedEntity)
        {
            _context.Add(insertedEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProfessorEntity updatedEntity)
        {
            try
            {
                _context.Update(updatedEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await GetByIdAsync(updatedEntity.Id) == null)
                {
                    throw new RepositoryException("Professor não encontrado!");
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
