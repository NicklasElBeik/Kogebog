using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Service.DTO.ProfileDTO
{
    public class ProfileResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<ProfileRecipeResponse> Recipes { get; set; } = [];
    }

    public class ProfileRecipeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int AmountOfServings { get; set; }
        public byte[]? Image { get; set; }
    }
}
