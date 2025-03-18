using Kogebog.API.Repository.Database.Entities;
using Kogebog.API.Repository.Repositories.Interfaces;
using Kogebog.API.Service.DTO.IngredientDTO;
using Kogebog.API.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kogebog.API.Service.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public IngredientResponse MapIngredientToIngredientResponse(Ingredient ingredient)
        {
            return new IngredientResponse
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
            };
        }


        public Ingredient MapIngredientRequestToIngredient(IngredientRequest ingredientRequest)
        {
            return new Ingredient
            {
                Name = ingredientRequest.Name,
            };
        }

        public async Task<IEnumerable<IngredientResponse>> GetAllAsync()
        {
            IEnumerable<Ingredient> ingredients = await _ingredientRepository.GetAllAsync();
            return ingredients.Select(MapIngredientToIngredientResponse).ToList();
        }

        public async Task<IngredientResponse?> GetByIdAsync(Guid id)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(id);
            return ingredient is null ? null : MapIngredientToIngredientResponse(ingredient);
        }

        public async Task<IngredientResponse> AddAsync(IngredientRequest newIngredientRequest)
        {
            var ingredient = MapIngredientRequestToIngredient(newIngredientRequest);
            var insertedIngredient = await _ingredientRepository.AddAsync(ingredient);
            return MapIngredientToIngredientResponse(insertedIngredient);
        }

        public async Task<IngredientResponse> UpdateByIdAsync(Guid id, IngredientRequest updatedIngredientRequest)
        {
            var ingredient = MapIngredientRequestToIngredient(updatedIngredientRequest);
            var updatedIngredient = await _ingredientRepository.UpdateByIdAsync(id, ingredient);
            return MapIngredientToIngredientResponse(updatedIngredient);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await _ingredientRepository.DeleteByIdAsync(id);
        }
    }
}
