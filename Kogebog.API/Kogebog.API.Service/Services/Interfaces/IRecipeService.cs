using Kogebog.API.Service.DTO.RecipeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Service.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeResponse>> GetAllAsync();
        Task<RecipeResponse?> GetByIdAsync(Guid id);
        Task<RecipeResponse?> GetByProfileIdAsync(Guid profileId);
        Task<RecipeResponse> AddAsync(RecipeRequest newRecipeRequest);
        Task<RecipeResponse> UpdateByIdAsync(Guid id, RecipeRequest updatedRecipeRequest);
        Task DeleteByIdAsync(Guid id);
    }
}
