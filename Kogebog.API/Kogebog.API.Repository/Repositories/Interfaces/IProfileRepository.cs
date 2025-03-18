using Kogebog.API.Repository.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Repository.Repositories.Interfaces
{
    public interface IProfileRepository
    {
        Task<IEnumerable<Profile>> GetAllAsync();
        Task<Profile?> GetByIdAsync(Guid id);
        Task<Profile> AddAsync(Profile newProfile);
        Task<Profile> UpdateByIdAsync(Guid id, Profile updatedProfile);
        Task DeleteByIdAsync(Guid id);
    }
}
