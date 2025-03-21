using Kogebog.API.Service.DTO.RecipeDTO;
using Kogebog.API.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kogebog.API.ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<RecipeResponse> recipes = await _recipeService.GetAllAsync();

                if (!recipes.Any())
                {
                    return NoContent();
                }

                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] RecipeRequest newRecipe)
        {
            try
            {
                return Ok(await _recipeService.AddAsync(newRecipe));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{recipeId}")]
        public async Task<IActionResult> FindById([FromRoute] Guid recipeId)
        {
            try
            {
                var recipeResponse = await _recipeService.GetByIdAsync(recipeId);

                if (recipeResponse == null)
                {
                    return NotFound();
                }

                return Ok(recipeResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("profile/{profileId}")]
        public async Task<IActionResult> FindByProfileId([FromRoute] Guid profileId)
        {
            try
            {
                var recipeResponse = await _recipeService.GetByProfileIdAsync(profileId);

                if (recipeResponse == null)
                {
                    return NotFound();
                }

                return Ok(recipeResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{recipeId}")]
        public async Task<IActionResult> UpdateById([FromRoute] Guid recipeId, [FromForm] RecipeRequest updateRecipe)
        {
            try
            {
                var recipeResponse = await _recipeService.UpdateByIdAsync(recipeId, updateRecipe);

                if (recipeResponse == null)
                {
                    return NotFound();
                }

                return Ok(recipeResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{recipeId}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid recipeId)
        {
            try
            {
                await _recipeService.DeleteByIdAsync(recipeId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
