using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Service.DTO.RecipeDTO
{
    public class RecipeRequest
    {
        [Required]
        [StringLength(30, ErrorMessage = "Recipe name cannot be longer than 30 chars")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 8, ErrorMessage = "Amount of servings must be between 1 and 8")]
        public required int AmountOfServings { get; set; }
        public IFormFile? Image { get; set; }
    }
}
