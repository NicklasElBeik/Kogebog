using Kogebog.API.Service.DTO.UnitDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Service.Services.Interfaces
{
    public interface IUnitService
    {
        Task<IEnumerable<UnitResponse>> GetAllAsync();
        Task<UnitResponse?> GetByIdAsync(Guid id);
        Task<UnitResponse> AddAsync(UnitRequest newUnitRequest);
        Task<UnitResponse> UpdateByIdAsync(Guid id, UnitRequest updatedUnitRequest);
        Task DeleteByIdAsync(Guid id);
    }
}
