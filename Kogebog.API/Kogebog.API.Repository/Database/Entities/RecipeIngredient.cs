using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Repository.Database.Entities
{
    public class RecipeIngredient
    {
        public Guid Id { get; set; }
        public double Quantity { get; set; }
        public Guid UnitId { get; set; }
        public Unit? Unit { get; set; }
        public Guid RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
        public Guid IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }
    }
}
