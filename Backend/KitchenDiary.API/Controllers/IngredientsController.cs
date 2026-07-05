using KitchenDiary.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using KitchenDiary.API.DTOs;
namespace KitchenDiary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly IIngredientService _ingredientService;

    public IngredientsController(IIngredientService ingredientService)
    {
        _ingredientService=ingredientService;
    }
    [HttpPost("{recipeId}")]
    public async Task<ActionResult<IngredientDto>> AddIngredient(int id, CreateIngredientDto ingredientDto)
    {
        var createdIngredient = await _ingredientService.AddIngredientAsync(id, ingredientDto);
        if(createdIngredient==null)
        {
            return NotFound();
        }

        return CreatedAtAction(nameof(AddIngredient), new { id = createdIngredient.Id },
        createdIngredient);
    }
    [HttpDelete("{ingredientId}")]
    public async Task<IActionResult> DeleteIngredient(int ingredientId)
    {
        var deleted = await _ingredientService.DeleteIngredientAsync(ingredientId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}