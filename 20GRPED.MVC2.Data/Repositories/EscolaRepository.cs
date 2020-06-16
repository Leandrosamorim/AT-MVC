using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20GRPED.MVC2.Data.Context;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Exceptions;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace _20GRPED.MVC2.Data.Repositories
{
    public class EscolaRepository : IEscolaRepository
    {
        private readonly BibliotecaContext _context;

        public EscolaRepository(
            BibliotecaContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            var autorModel = await _context.Escolas.FindAsync(id);
            _context.Escolas.Remove(autorModel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EscolaEntity>> GetAllAsync()
        {
            return await _context.Escolas.ToListAsync();
        }

        public async Task<EscolaEntity> GetByIdAsync(int id)
        {
            return await _context.Escolas
                .Include(x => x.Professores)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(EscolaEntity insertedEntity)
        {
            _context.Add(insertedEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EscolaEntity updatedEntity)
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
                    throw new RepositoryException("Livro não encontrado!");
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
