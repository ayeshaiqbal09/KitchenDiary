using KitchenDiary.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using KitchenDiary.API.DTOs;
namespace KitchenDiary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    private readonly IIngredientService _ingredientService;

    public RecipesController(IRecipeService recipeService, IIngredientService ingredientService)
    {
        _recipeService = recipeService;
        _ingredientService=ingredientService;
    }
    [HttpPost]
    public async Task<ActionResult<RecipeDto>> CreateRecipe(CreateRecipeDto recipeDto)
    {
        var createdRecipe = await _recipeService.CreateRecipeAsync(recipeDto);

        return CreatedAtAction(nameof(CreateRecipe), new { id = createdRecipe.Id }, createdRecipe);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeDto>> GetRecipe(int id)
    {
        var recipe = await _recipeService.GetRecipeByIdAsync(id);

        if (recipe == null)
            return NotFound();

        return Ok(recipe);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<RecipeDto>> UpdateRecipe(int id, CreateRecipeDto recipeDto)
    {
        var updatedRecipe = await _recipeService.UpdateRecipeAsync(id, recipeDto);

        if (updatedRecipe == null)
            return NotFound();

        return Ok(updatedRecipe);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipe(int id)
    {
        var deleted = await _recipeService.DeleteRecipeAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> SearchRecipes(string searchTerm)
    {
        var recipes = await _recipeService.SearchRecipesAsync(searchTerm);

        return Ok(recipes);
    }
    
    [HttpPost("{recipeId}/ingredients")]
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
    [HttpDelete("ingridients/{ingredientId}")]
    public async Task<IActionResult> DeleteIngredient(int ingredientId)
    {
        var deleted = await _ingredientService.DeleteIngredientAsync(ingredientId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}