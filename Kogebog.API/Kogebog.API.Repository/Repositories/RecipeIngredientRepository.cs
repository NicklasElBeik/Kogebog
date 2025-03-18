using Kogebog.API.Repository.Database;
using Kogebog.API.Repository.Database.Entities;
using Kogebog.API.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Repository.Repositories
{
    public class RecipeIngredientRepository : IRecipeIngredientRepository
    {
        private readonly ApplicationDbContext _context;

        public RecipeIngredientRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<RecipeIngredient>> GetAllAsync()
        {
            return await _context.RecipeIngredients
                .Include(r => r.Ingredient)
                .Include(r => r.Recipe)
                .Include(r => r.Unit)
                .ToListAsync();
        }

        public async Task<RecipeIngredient?> GetByIdAsync(Guid id)
        {
            return await _context.RecipeIngredients
                .Include(r => r.Ingredient)
                .Include(r => r.Recipe)
                .Include(r => r.Unit)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<RecipeIngredient> AddAsync(RecipeIngredient newRecipeIngredient)
        {
            await _context.RecipeIngredients.AddAsync(newRecipeIngredient);
            await _context.SaveChangesAsync();
            var recipeIngredient = await GetByIdAsync(newRecipeIngredient.Id);

            if (recipeIngredient is null)
                throw new Exception("RecipeIngredient wasn't added");

            return recipeIngredient;
        }

        public async Task<RecipeIngredient> UpdateByIdAsync(Guid id, RecipeIngredient updatedRecipeIngredient)
        {
            var existingRecipeIngredient = await GetByIdAsync(id);

            if (existingRecipeIngredient is null)
                throw new Exception("RecipeIngredient not found");

            existingRecipeIngredient.Quantity = updatedRecipeIngredient.Quantity;
            existingRecipeIngredient.UnitId = updatedRecipeIngredient.UnitId;


            await _context.SaveChangesAsync();

            return existingRecipeIngredient;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var existingRecipeIngredient = await _context.RecipeIngredients.FindAsync(id);
            if (existingRecipeIngredient != null)
            {
                _context.RecipeIngredients.Remove(existingRecipeIngredient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
