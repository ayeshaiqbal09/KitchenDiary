using KitchenDiary.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using KitchenDiary.API.DTOs;
namespace KitchenDiary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly IRecipeService _recipeService;

    public RecipesController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }
    [HttpPost]
    public async Task<ActionResult<RecipeDto>> CreateRecipe(CreateRecipeDto recipeDto)
    {
        var recipe = await _recipeService.CreateRecipeAsync(recipeDto);

        return CreatedAtAction(nameof(CreateRecipe), new { id = recipe.Id }, recipe);
    }
}