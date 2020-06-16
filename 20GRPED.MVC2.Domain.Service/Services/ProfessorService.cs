using System;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Exceptions;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _20GRPED.MVC2.Domain.Service.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;

        public ProfessorService(
            IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public async Task DeleteAsync(int id)
        {
            await _professorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProfessorEntity>> GetAllAsync()
        {
            return await _professorRepository.GetAllAsync();
        }

        public async Task<ProfessorEntity> GetByIdAsync(int id)
        {
            return await _professorRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(ProfessorEntity insertedEntity)
        {

            await _professorRepository.InsertAsync(insertedEntity);
        }

        public async Task UpdateAsync(ProfessorEntity updatedEntity)
        {
            await _professorRepository.UpdateAsync(updatedEntity);
        }

    }
}
