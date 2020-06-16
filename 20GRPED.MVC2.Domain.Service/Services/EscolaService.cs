using System.Collections.Generic;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;

namespace _20GRPED.MVC2.Domain.Service.Services
{
    public class EscolaService : IEscolaService
    {
        private readonly IEscolaRepository _escolaRepository;

        public EscolaService(
            IEscolaRepository escolaRepository)
        {
            _escolaRepository = escolaRepository;
        }

        public async Task DeleteAsync(int id)
        {
            await _escolaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<EscolaEntity>> GetAllAsync()
        {
            return await _escolaRepository.GetAllAsync();
        }

        public async Task<EscolaEntity> GetByIdAsync(int id)
        {
            return await _escolaRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(EscolaEntity insertedEntity)
        {
            await _escolaRepository.InsertAsync(insertedEntity);
        }

        public async Task UpdateAsync(EscolaEntity updatedEntity)
        {
            await _escolaRepository.UpdateAsync(updatedEntity);
        }
    }
}
