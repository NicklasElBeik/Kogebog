using Kogebog.API.Repository.Database.Entities;
using Kogebog.API.Repository.Repositories.Interfaces;
using Kogebog.API.Service.DTO.IngredientDTO;
using Kogebog.API.Service.DTO.RecipeDTO;
using Kogebog.API.Service.DTO.UnitDTO;
using Kogebog.API.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kogebog.API.Service.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public RecipeResponse MapRecipeToRecipeResponse(Recipe recipe)
        {
            return new RecipeResponse
            {
                Id = recipe.Id,
                Image = recipe.Image,
                AmountOfServings = recipe.AmountOfServings,
                Profile = recipe.Profile != null ? new RecipeProfileResponse
                {
                    Id = recipe.Profile.Id,
                    Name = recipe.Profile.Name,
                } : null,
                RecipeIngredients = recipe.RecipeIngredients.Select(recipeIngredient => new RecipeRecipeIngredientResponse
                {
                    Id = recipeIngredient.Id,
                    Quantity = recipeIngredient.Quantity,
                    Ingredient = recipeIngredient.Ingredient != null ? new IngredientResponse
                    {

                    } : null,
                    Unit = recipeIngredient.Unit != null ? new UnitResponse
                    {
                        Id = recipeIngredient.Unit.Id,
                        Name = recipeIngredient.Unit.Name,
                        PluralName = recipeIngredient.Unit.PluralName,
                        Abbreviation = recipeIngredient.Unit.Abbreviation
                    } : null
                })

            };
        }


        public Recipe MapRecipeRequestToRecipe(RecipeRequest recipeRequest)
        {
            var recipe = new Recipe
            {
                Name = recipeRequest.Name,
                AmountOfServings = recipeRequest.AmountOfServings,
            };

            if (recipeRequest.Image != null && recipeRequest.Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    recipeRequest.Image.CopyTo(memoryStream);

                    recipe.Image = memoryStream.ToArray();
                }
            }
            else
                recipe.Image = null;

            return recipe;
        }

        public async Task<IEnumerable<RecipeResponse>> GetAllAsync()
        {
            IEnumerable<Recipe> recipes = await _recipeRepository.GetAllAsync();
            return recipes.Select(MapRecipeToRecipeResponse).ToList();
        }

        public async Task<RecipeResponse?> GetByIdAsync(Guid id)
        {
            var recipe = await _recipeRepository.GetByIdAsync(id);
            return recipe is null ? null : MapRecipeToRecipeResponse(recipe);
        }

        public async Task<RecipeResponse> AddAsync(RecipeRequest newRecipeRequest)
        {
            var recipe = MapRecipeRequestToRecipe(newRecipeRequest);
            var insertedRecipe = await _recipeRepository.AddAsync(recipe);
            return MapRecipeToRecipeResponse(insertedRecipe);
        }

        public async Task<RecipeResponse> UpdateByIdAsync(Guid id, RecipeRequest updatedRecipeRequest)
        {
            var recipe = MapRecipeRequestToRecipe(updatedRecipeRequest);
            var updatedRecipe = await _recipeRepository.UpdateByIdAsync(id, recipe);
            return MapRecipeToRecipeResponse(updatedRecipe);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await _recipeRepository.DeleteByIdAsync(id);
        }
    }
}
