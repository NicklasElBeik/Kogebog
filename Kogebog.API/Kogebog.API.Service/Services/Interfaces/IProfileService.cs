using Kogebog.API.Service.DTO.ProfileDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Service.Services.Interfaces
{
    public interface IProfileService
    {
        Task<IEnumerable<ProfileResponse>> GetAllAsync();
        Task<ProfileResponse?> GetByIdAsync(Guid id);
        Task<ProfileResponse> AddAsync(ProfileRequest newProfileRequest);
        Task<ProfileResponse> UpdateByIdAsync(Guid id, ProfileRequest updatedProfileRequest);
        Task DeleteByIdAsync(Guid id);
    }
}
