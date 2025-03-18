using Kogebog.API.Repository.Database.Entities;
using Kogebog.API.Repository.Repositories.Interfaces;
using Kogebog.API.Service.DTO.IngredientDTO;
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
    public class RecipeIngredientService : IRecipeIngredientService
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;

        public RecipeIngredientService(IRecipeIngredientRepository recipeIngredientRepository)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
        }

        public RecipeIngredientResponse MapRecipeIngredientToRecipeIngredientResponse(RecipeIngredient recipeIngredient)
        {
            return new RecipeIngredientResponse
            {
                Id = recipeIngredient.Id,
                Quantity = recipeIngredient.Quantity,
                Unit = recipeIngredient.Unit != null ? new UnitResponse
                {
                    Id = recipeIngredient.Unit.Id,
                    Name = recipeIngredient.Unit.Name,
                    PluralName = recipeIngredient.Unit.PluralName,
                    Abbreviation = recipeIngredient.Unit.Abbreviation
                } : null,
                Recipe = recipeIngredient.Recipe != null ? new RecipeIngredientRecipeResponse
                {
                    Id = recipeIngredient.Recipe.Id,
                    Name = recipeIngredient.Recipe.Name,
                    AmountOfServings = recipeIngredient.Recipe.AmountOfServings,
                    Profile = recipeIngredient.Recipe.Profile != null ? new RecipeIngredientRecipeProfileResponse
                    {
                        Id = recipeIngredient.Recipe.Profile.Id,
                        Name = recipeIngredient.Recipe.Profile.Name,
                    } : null
                } : null,
                Ingredient = recipeIngredient.Ingredient != null ? new IngredientResponse
                {
                    Id = recipeIngredient.Ingredient.Id,
                    Name = recipeIngredient.Ingredient.Name
                } : null
            };
        }


        public RecipeIngredient MapRecipeIngredientRequestToRecipeIngredient(RecipeIngredientRequest recipeIngredientRequest)
        {
            return new RecipeIngredient
            {
                Quantity = recipeIngredientRequest.Quantity,
                UnitId = recipeIngredientRequest.UnitId,
                RecipeId = recipeIngredientRequest.RecipeId,
                IngredientId = recipeIngredientRequest.IngredientId,
            };
        }

        public async Task<IEnumerable<RecipeIngredientResponse>> GetAllAsync()
        {
            IEnumerable<RecipeIngredient> recipeIngredients = await _recipeIngredientRepository.GetAllAsync();
            return recipeIngredients.Select(MapRecipeIngredientToRecipeIngredientResponse).ToList();
        }

        public async Task<RecipeIngredientResponse?> GetByIdAsync(Guid id)
        {
            var recipeIngredient = await _recipeIngredientRepository.GetByIdAsync(id);
            return recipeIngredient is null ? null : MapRecipeIngredientToRecipeIngredientResponse(recipeIngredient);
        }

        public async Task<RecipeIngredientResponse> AddAsync(RecipeIngredientRequest newRecipeIngredientRequest)
        {
            var recipeIngredient = MapRecipeIngredientRequestToRecipeIngredient(newRecipeIngredientRequest);
            var insertedRecipeIngredient = await _recipeIngredientRepository.AddAsync(recipeIngredient);
            return MapRecipeIngredientToRecipeIngredientResponse(insertedRecipeIngredient);
        }

        public async Task<RecipeIngredientResponse> UpdateByIdAsync(Guid id, RecipeIngredientRequest updatedRecipeIngredientRequest)
        {
            var recipeIngredient = MapRecipeIngredientRequestToRecipeIngredient(updatedRecipeIngredientRequest);
            var updatedRecipeIngredient = await _recipeIngredientRepository.UpdateByIdAsync(id, recipeIngredient);
            return MapRecipeIngredientToRecipeIngredientResponse(updatedRecipeIngredient);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await _recipeIngredientRepository.DeleteByIdAsync(id);
        }
    }
}
