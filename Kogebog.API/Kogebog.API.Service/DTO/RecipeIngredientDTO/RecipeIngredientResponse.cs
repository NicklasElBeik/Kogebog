using Kogebog.API.Service.DTO.IngredientDTO;
using Kogebog.API.Service.DTO.UnitDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Service.DTO.RecipeIngredientDTO
{
    public class RecipeIngredientResponse
    {
        public Guid Id { get; set; }
        public double Quantity { get; set; }
        public UnitResponse? Unit { get; set; }
        public RecipeIngredientRecipeResponse? Recipe { get; set; }
        public IngredientResponse? Ingredient { get; set; }

    }

    public class RecipeIngredientRecipeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int AmountOfServings { get; set; }
        public RecipeIngredientRecipeProfileResponse? Profile { get; set; }
    }

    public class RecipeIngredientRecipeProfileResponse
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
