using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Service.DTO.UnitDTO
{
    public class UnitResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string PluralName {  get; set; }
        public required string Abbreviation { get; set; }
    }
}
