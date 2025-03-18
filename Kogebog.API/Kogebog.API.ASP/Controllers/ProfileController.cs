using Kogebog.API.Service.DTO.ProfileDTO;
using Kogebog.API.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kogebog.API.ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<ProfileResponse> profiles = await _profileService.GetAllAsync();

                if (!profiles.Any())
                {
                    return NoContent();
                }

                return Ok(profiles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProfileRequest newProfile)
        {
            try
            {
                return Ok(await _profileService.AddAsync(newProfile));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{profileId}")]
        public async Task<IActionResult> FindById([FromRoute] Guid profileId)
        {
            try
            {
                var profileResponse = await _profileService.GetByIdAsync(profileId);

                if (profileResponse == null)
                {
                    return NotFound();
                }

                return Ok(profileResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{profileId}")]
        public async Task<IActionResult> UpdateById([FromRoute] Guid profileId, [FromBody] ProfileRequest updateProfile)
        {
            try
            {
                var profileResponse = await _profileService.UpdateByIdAsync(profileId, updateProfile);

                if (profileResponse == null)
                {
                    return NotFound();
                }

                return Ok(profileResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{profileId}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid profileId)
        {
            try
            {
                await _profileService.DeleteByIdAsync(profileId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
