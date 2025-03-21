using Kogebog.API.Repository.Database.Entities;
using Kogebog.API.Repository.Repositories.Interfaces;
using Kogebog.API.Service.DTO.IngredientDTO;
using Kogebog.API.Service.DTO.RecipeDTO;
using Kogebog.API.Service.DTO.RecipeIngredientDTO;
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
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;

        public RecipeService(IRecipeRepository recipeRepository, IRecipeIngredientRepository recipeIngredientRepository)
        {
            _recipeRepository = recipeRepository;
            _recipeIngredientRepository = recipeIngredientRepository;
        }

        public RecipeResponse MapRecipeToRecipeResponse(Recipe recipe)
        {
            return new RecipeResponse
            {
                Id = recipe.Id,
                Name = recipe.Name,
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
                        Id = recipeIngredient.Ingredient.Id,
                        Name = recipeIngredient.Ingredient.Name
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
                ProfileId = recipeRequest.ProfileId
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

        public List<RecipeIngredient> MapRecipeRecipeIngredientRequestToRecipeIngredientList(List<RecipeRecipeIngredientRequest> recipeRecipeIngredientRequests, Guid recipeId)
        {
            return recipeRecipeIngredientRequests.Select(recipeRecipeIngredientRequest => new RecipeIngredient
            {
                Quantity = recipeRecipeIngredientRequest.Quantity,
                UnitId = recipeRecipeIngredientRequest.UnitId,
                RecipeId = recipeId,
                IngredientId = recipeRecipeIngredientRequest.IngredientId,
            }).ToList();
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

        public async Task<IEnumerable<RecipeResponse>> GetByProfileIdAsync(Guid profileId)
        {
            var recipes = await _recipeRepository.GetByProfileIdAsync(profileId);
            return recipes.Select(MapRecipeToRecipeResponse).ToList();
        }

        public async Task<RecipeResponse> AddAsync(RecipeRequest newRecipeRequest)
        {
            var recipe = MapRecipeRequestToRecipe(newRecipeRequest);
            var insertedRecipe = await _recipeRepository.AddAsync(recipe);

            await _recipeIngredientRepository.AddRangeAsync(MapRecipeRecipeIngredientRequestToRecipeIngredientList(newRecipeRequest.RecipeIngredients, insertedRecipe.Id));

            var finishedRecipe = await _recipeRepository.GetByIdAsync(insertedRecipe.Id);

            if (finishedRecipe is null)
                throw new Exception("Recipe wasn't found.");

            return MapRecipeToRecipeResponse(finishedRecipe);
        }

        public async Task<RecipeResponse> UpdateByIdAsync(Guid id, RecipeRequest updatedRecipeRequest)
        {
            var recipe = MapRecipeRequestToRecipe(updatedRecipeRequest);
            var updatedRecipe = await _recipeRepository.UpdateByIdAsync(id, recipe);

            var recipeIngredientsToCreate = updatedRecipeRequest.RecipeIngredients.Where(x => !Guid.TryParse(x.Id, out var _)).ToList();

            await _recipeIngredientRepository.AddRangeAsync(MapRecipeRecipeIngredientRequestToRecipeIngredientList(recipeIngredientsToCreate, updatedRecipe.Id));

            var recipeIngredientsToDelete = updatedRecipeRequest.RecipeIngredients
                .Where(x => Guid.TryParse(x.Id, out var guidId) &&
                            !updatedRecipe.RecipeIngredients.Any(y => y.Id == guidId))
                .ToList();

            foreach (var recipeIngredient in recipeIngredientsToDelete)
            {
                if(Guid.TryParse(recipeIngredient.Id, out var guidId)) {
                    await _recipeIngredientRepository.DeleteByIdAsync(guidId);
                }
            }

            var finishedRecipe = await _recipeRepository.GetByIdAsync(updatedRecipe.Id);

            if (finishedRecipe is null)
                throw new Exception("Recipe wasn't found.");

            return MapRecipeToRecipeResponse(finishedRecipe);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await _recipeRepository.DeleteByIdAsync(id);
        }
    }
}
