using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Service.DTO.RecipeIngredientDTO
{
    public class RecipeIngredientRequest
    {

        [Required]
        [Range(0, 10000, ErrorMessage = "Quantity must be between 0 and 10.000")]
        public required double Quantity { get; set; }

        [Required(ErrorMessage = "Unit is required")]
        public Guid UnitId { get; set; }

        [Required(ErrorMessage = "Recipe is required")]
        public Guid RecipeId { get; set; }

        [Required(ErrorMessage = "Ingredient is required")]
        public Guid IngredientId { get; set; }
    }
}
