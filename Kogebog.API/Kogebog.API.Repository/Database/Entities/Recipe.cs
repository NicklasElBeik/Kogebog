using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Repository.Database.Entities
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int AmountOfPeople { get; set; }
        public byte[]? Image { get; set; }
        public Guid ProfileId { get; set; }
        public Profile? Profile { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; } = [];
    }
}
