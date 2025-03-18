using Kogebog.API.Repository.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Repository.Repositories.Interfaces
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetAllAsync();
        Task<Recipe?> GetByIdAsync(Guid id);
        Task<Recipe> AddAsync(Recipe newRecipe);
        Task<Recipe> UpdateByIdAsync(Guid id, Recipe updatedRecipe);
        Task DeleteByIdAsync(Guid id);
    }
}
