using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Service.DTO.UnitDTO
{
    public class UnitRequest
    {
        [Required]
        [StringLength(30, ErrorMessage = "Unit name cannot be longer than 30 chars")]
        public required string Name { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Unit plural name cannot be longer than 30 chars")]
        public required string PluralName { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Unit abbreviation cannot be longer than 30 chars")]
        public required string Abbreviation { get; set; }
    }
}
