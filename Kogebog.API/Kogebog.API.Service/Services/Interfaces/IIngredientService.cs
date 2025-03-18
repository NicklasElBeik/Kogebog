using Kogebog.API.Service.DTO.IngredientDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Service.Services.Interfaces
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientResponse>> GetAllAsync();
        Task<IngredientResponse?> GetByIdAsync(Guid id);
        Task<IngredientResponse> AddAsync(IngredientRequest newIngredientRequest);
        Task<IngredientResponse> UpdateByIdAsync(Guid id, IngredientRequest updatedIngredientRequest);
        Task DeleteByIdAsync(Guid id);
    }
}
