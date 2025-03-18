using Kogebog.API.Repository.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Repository.Repositories.Interfaces
{
    public interface IUnitRepository
    {
        Task<IEnumerable<Unit>> GetAllAsync();
        Task<Unit?> GetByIdAsync(Guid id);
        Task<Unit> AddAsync(Unit newUnit);
        Task<Unit> UpdateByIdAsync(Guid id, Unit updatedUnit);
        Task DeleteByIdAsync(Guid id);
    }
}
