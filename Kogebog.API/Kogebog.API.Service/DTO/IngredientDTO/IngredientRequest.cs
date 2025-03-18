using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Service.DTO.IngredientDTO
{
    public class IngredientRequest
    {
        [Required]
        [StringLength(50, ErrorMessage = "Ingredient name cannot be longer than 50 chars")]
        public required string Name { get; set; }
    }
}
