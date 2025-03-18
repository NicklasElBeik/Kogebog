using Kogebog.API.Repository.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Repository.Repositories.Interfaces
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<Ingredient>> GetAllAsync();
        Task<Ingredient?> GetByIdAsync(Guid id);
        Task<Ingredient> AddAsync(Ingredient newIngredient);
        Task<Ingredient> UpdateByIdAsync(Guid id, Ingredient updatedIngredient);
        Task DeleteByIdAsync(Guid id);
    }
}
