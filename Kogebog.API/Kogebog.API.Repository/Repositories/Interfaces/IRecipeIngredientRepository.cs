using Kogebog.API.Repository.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Repository.Repositories.Interfaces
{
    public interface IRecipeIngredientRepository
    {
        Task<IEnumerable<RecipeIngredient>> GetAllAsync();
        Task<RecipeIngredient?> GetByIdAsync(Guid id);
        Task<RecipeIngredient?> GetByRecipeIdAsync(Guid recipeId);
        Task<RecipeIngredient> AddAsync(RecipeIngredient newRecipeIngredient);
        Task<List<RecipeIngredient>> AddRangeAsync(List<RecipeIngredient> newRecipeIngredients);
        Task<RecipeIngredient> UpdateByIdAsync(Guid id, RecipeIngredient updatedRecipeIngredient);
        Task DeleteByIdAsync(Guid id);
    }
}
