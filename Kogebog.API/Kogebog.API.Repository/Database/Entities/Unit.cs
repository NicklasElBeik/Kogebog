using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Repository.Database.Entities
{
    public class Unit
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string PluralName { get; set; }
        public required string Abbreviation { get; set; }
    }
}
