using System.Collections.Generic;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;

namespace _20GRPED.MVC2.Domain.Model.Interfaces.Repositories
{
    public interface IEscolaRepository
    {
        Task<IEnumerable<EscolaEntity>> GetAllAsync();
        Task<EscolaEntity> GetByIdAsync(int id);
        Task InsertAsync(EscolaEntity insertedEntity);
        Task UpdateAsync(EscolaEntity updatedEntity);
        Task DeleteAsync(int id);
    }
}
