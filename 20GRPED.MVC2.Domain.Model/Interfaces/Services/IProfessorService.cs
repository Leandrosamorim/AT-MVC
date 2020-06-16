using System.Collections.Generic;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;

namespace _20GRPED.MVC2.Domain.Model.Interfaces.Services
{
    public interface IProfessorService
    {
        Task<IEnumerable<ProfessorEntity>> GetAllAsync();
        Task<ProfessorEntity> GetByIdAsync(int id);
        Task InsertAsync(ProfessorEntity insertedEntity);
        Task UpdateAsync(ProfessorEntity updatedEntity);
        Task DeleteAsync(int id);
    }
}
