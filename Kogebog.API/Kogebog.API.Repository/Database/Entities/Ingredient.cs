using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Repository.Database.Entities
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; } = [];
    }
}
