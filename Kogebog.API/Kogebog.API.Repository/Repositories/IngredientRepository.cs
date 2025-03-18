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
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ApplicationDbContext _context;

        public IngredientRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            return await _context.Ingredients
                .Include(i => i.RecipeIngredients)
                .ToListAsync();
        }

        public async Task<Ingredient?> GetByIdAsync(Guid id)
        {
            return await _context.Ingredients
                .Include(i => i.RecipeIngredients)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Ingredient> AddAsync(Ingredient newIngredient)
        {
            await _context.Ingredients.AddAsync(newIngredient);
            await _context.SaveChangesAsync();
            var ingredient = await GetByIdAsync(newIngredient.Id);

            if (ingredient is null)
                throw new Exception("Ingredient wasn't added");

            return ingredient;
        }

        public async Task<Ingredient> UpdateByIdAsync(Guid id, Ingredient updatedIngredient)
        {
            var existingIngredient = await GetByIdAsync(id);

            if (existingIngredient is null)
                throw new Exception("Ingredient not found");

            existingIngredient.Name = updatedIngredient.Name;

            await _context.SaveChangesAsync();

            return existingIngredient;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var existingIngredient = await _context.Ingredients.FindAsync(id);
            if (existingIngredient != null)
            {
                _context.Ingredients.Remove(existingIngredient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
