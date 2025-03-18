using Kogebog.API.Service.DTO.UnitDTO;
using Kogebog.API.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kogebog.API.ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<UnitResponse> units = await _unitService.GetAllAsync();

                if (!units.Any())
                {
                    return NoContent();
                }

                return Ok(units);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UnitRequest newUnit)
        {
            try
            {
                return Ok(await _unitService.AddAsync(newUnit));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{unitId}")]
        public async Task<IActionResult> FindById([FromRoute] Guid unitId)
        {
            try
            {
                var unitResponse = await _unitService.GetByIdAsync(unitId);

                if (unitResponse == null)
                {
                    return NotFound();
                }

                return Ok(unitResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{unitId}")]
        public async Task<IActionResult> UpdateById([FromRoute] Guid unitId, [FromBody] UnitRequest updateUnit)
        {
            try
            {
                var unitResponse = await _unitService.UpdateByIdAsync(unitId, updateUnit);

                if (unitResponse == null)
                {
                    return NotFound();
                }

                return Ok(unitResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{unitId}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid unitId)
        {
            try
            {
                await _unitService.DeleteByIdAsync(unitId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
