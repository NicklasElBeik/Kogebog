using Kogebog.API.Service.DTO.RecipeIngredientDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Service.Services.Interfaces
{
    public interface IRecipeIngredientService
    {
        Task<IEnumerable<RecipeIngredientResponse>> GetAllAsync();
        Task<RecipeIngredientResponse?> GetByIdAsync(Guid id);
        Task<RecipeIngredientResponse?> GetByRecipeIdAsync(Guid recipeId);
        Task<RecipeIngredientResponse> AddAsync(RecipeIngredientRequest newRecipeIngredientRequest);
        Task<RecipeIngredientResponse> UpdateByIdAsync(Guid id, RecipeIngredientRequest updatedRecipeIngredientRequest);
        Task DeleteByIdAsync(Guid id);
    }
}
