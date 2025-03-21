using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Service.DTO.ProfileDTO
{
    public class ProfileRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Name cannot be longer than 30 chars")]
        public string Name { get; set; } = string.Empty;
    }
}
