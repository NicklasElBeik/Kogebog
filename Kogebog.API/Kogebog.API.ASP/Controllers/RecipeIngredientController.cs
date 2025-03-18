using Kogebog.API.Service.DTO.RecipeIngredientDTO;
using Kogebog.API.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kogebog.API.ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeIngredientController : ControllerBase
    {
        private readonly IRecipeIngredientService _recipeIngredientService;

        public RecipeIngredientController(IRecipeIngredientService recipeIngredientService)
        {
            _recipeIngredientService = recipeIngredientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<RecipeIngredientResponse> recipeIngredients = await _recipeIngredientService.GetAllAsync();

                if (!recipeIngredients.Any())
                {
                    return NoContent();
                }

                return Ok(recipeIngredients);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RecipeIngredientRequest newRecipeIngredient)
        {
            try
            {
                return Ok(await _recipeIngredientService.AddAsync(newRecipeIngredient));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{recipeIngredientId}")]
        public async Task<IActionResult> FindById([FromRoute] Guid recipeIngredientId)
        {
            try
            {
                var recipeIngredientResponse = await _recipeIngredientService.GetByIdAsync(recipeIngredientId);

                if (recipeIngredientResponse == null)
                {
                    return NotFound();
                }

                return Ok(recipeIngredientResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{recipeIngredientId}")]
        public async Task<IActionResult> UpdateById([FromRoute] Guid recipeIngredientId, [FromBody] RecipeIngredientRequest updateRecipeIngredient)
        {
            try
            {
                var recipeIngredientResponse = await _recipeIngredientService.UpdateByIdAsync(recipeIngredientId, updateRecipeIngredient);

                if (recipeIngredientResponse == null)
                {
                    return NotFound();
                }

                return Ok(recipeIngredientResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{recipeIngredientId}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid recipeIngredientId)
        {
            try
            {
                await _recipeIngredientService.DeleteByIdAsync(recipeIngredientId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
