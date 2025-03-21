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
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationDbContext _context;

        public RecipeRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            return await _context.Recipes
                .Include(r => r.Profile)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(r => r.Ingredient)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(r => r.Unit)
                .ToListAsync();
        }

        public async Task<Recipe?> GetByIdAsync(Guid id)
        {
            return await _context.Recipes
                .Include(r => r.Profile)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(r => r.Ingredient)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(r => r.Unit)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Recipe>> GetByProfileIdAsync(Guid id)
        {
            return await _context.Recipes
                .Include(r => r.Profile)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(r => r.Ingredient)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(r => r.Unit)
                .Where(r => r.ProfileId == id)
                .ToListAsync();
        }

        public async Task<Recipe> AddAsync(Recipe newRecipe)
        {
            await _context.Recipes.AddAsync(newRecipe);
            await _context.SaveChangesAsync();
            var recipe = await GetByIdAsync(newRecipe.Id);

            if (recipe is null)
                throw new Exception("Recipe wasn't added");

            return recipe;
        }

        public async Task<Recipe> UpdateByIdAsync(Guid id, Recipe updatedRecipe)
        {
            var existingRecipe = await GetByIdAsync(id);

            if (existingRecipe is null)
                throw new Exception("Recipe not found");

            existingRecipe.Name = updatedRecipe.Name;
            existingRecipe.Image = updatedRecipe.Image;
            existingRecipe.AmountOfServings = updatedRecipe.AmountOfServings;

            await _context.SaveChangesAsync();

            return existingRecipe;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var existingRecipe = await _context.Recipes.FindAsync(id);
            if (existingRecipe != null)
            {
                _context.Recipes.Remove(existingRecipe);
                await _context.SaveChangesAsync();
            }
        }
    }
}
