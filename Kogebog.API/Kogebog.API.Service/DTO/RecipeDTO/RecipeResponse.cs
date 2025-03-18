using Kogebog.API.Service.DTO.IngredientDTO;
using Kogebog.API.Service.DTO.UnitDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Service.DTO.RecipeDTO
{
    public class RecipeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int AmountOfServings { get; set; }
        public byte[]? Image { get; set; }
        public RecipeProfileResponse? Profile { get; set; }
        public IEnumerable<RecipeRecipeIngredientResponse> RecipeIngredients { get; set; } = [];
    }

    public class RecipeProfileResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class RecipeRecipeIngredientResponse
    {
        public Guid Id { get; set; }
        public double Quantity { get; set; }
        public IngredientResponse? Ingredient { get; set; }
        public UnitResponse? Unit { get; set; }

    }
}
