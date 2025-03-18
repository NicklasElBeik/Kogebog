using Kogebog.API.Service.DTO.IngredientDTO;
using Kogebog.API.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kogebog.API.ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<IngredientResponse> ingredients = await _ingredientService.GetAllAsync();

                if (!ingredients.Any())
                {
                    return NoContent();
                }

                return Ok(ingredients);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] IngredientRequest newIngredient)
        {
            try
            {
                return Ok(await _ingredientService.AddAsync(newIngredient));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{ingredientId}")]
        public async Task<IActionResult> FindById([FromRoute] Guid ingredientId)
        {
            try
            {
                var ingredientResponse = await _ingredientService.GetByIdAsync(ingredientId);

                if (ingredientResponse == null)
                {
                    return NotFound();
                }

                return Ok(ingredientResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{ingredientId}")]
        public async Task<IActionResult> UpdateById([FromRoute] Guid ingredientId, [FromBody] IngredientRequest updateIngredient)
        {
            try
            {
                var ingredientResponse = await _ingredientService.UpdateByIdAsync(ingredientId, updateIngredient);

                if (ingredientResponse == null)
                {
                    return NotFound();
                }

                return Ok(ingredientResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{ingredientId}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid ingredientId)
        {
            try
            {
                await _ingredientService.DeleteByIdAsync(ingredientId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
