using KitchenDiary.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using KitchenDiary.API.DTOs;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using KitchenDiary.API.Models;
namespace KitchenDiary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    private readonly IIngredientService _ingredientService;
    private readonly IRecipeStepService _stepService;
    private readonly IImageService _imageService;
    private readonly ITagService _tagService;

    public RecipesController(IRecipeService recipeService, IIngredientService ingredientService, IRecipeStepService stepService,
    IImageService imageService, ITagService tagService)
    {
        _recipeService = recipeService;
        _ingredientService=ingredientService;
        _stepService=stepService;
        _imageService=imageService;
        _tagService = tagService;
    }
    [HttpPost]
    public async Task<ActionResult<RecipeDto>> CreateRecipe(CreateRecipeDto recipeDto)
    {
        var createdRecipe = await _recipeService.CreateRecipeAsync(recipeDto);

        return CreatedAtAction(nameof(CreateRecipe), new { id = createdRecipe.Id }, createdRecipe);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetAllRecipes()
    {
        var recipes = await _recipeService.GetAllRecipesAsync();
        return Ok(recipes);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeDto>> GetRecipeById(int id)
    {
        var recipe = await _recipeService.GetRecipeByIdAsync(id);

        if (recipe == null)
            return NotFound();

        return Ok(recipe);
    }
    [HttpPut("{recipeId}")]
    public async Task<ActionResult<RecipeDto>> UpdateRecipe(int recipeId, CreateRecipeDto recipeDto)
    {
        var updatedRecipe = await _recipeService.UpdateRecipeAsync(recipeId, recipeDto);

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
    public async Task<ActionResult<IngredientDto>> AddIngredient(int recipeId, CreateIngredientDto ingredientDto)
    {
        var createdIngredient = await _ingredientService.AddIngredientAsync(recipeId, ingredientDto);
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
    [HttpPut("ingredients/{ingredientId}")]
    public async Task<ActionResult<IngredientDto>> UpdateIngredient(
        int ingredientId,
        CreateIngredientDto ingredientDto)
    {
        var updatedIngredient =
            await _ingredientService.UpdateIngredientAsync(
                ingredientId,
                ingredientDto);

        if (updatedIngredient == null)
            return NotFound();

        return Ok(updatedIngredient);
    }
    [HttpPost("{recipeId}/recipeSteps")]
    public async Task<ActionResult<RecipeStepDto>> AddRecipeStep(int recipeId, CreateRecipeStepDto recipeStepDto)
    {
        var createdStep=await _stepService.AddRecipeStepAsync(recipeId, recipeStepDto);
        if(createdStep == null)
        {
            return NotFound();
        }
        return CreatedAtAction(nameof(AddRecipeStep), new { id = createdStep.Id },
        createdStep);
    }
    [HttpPut("steps/{stepId}")]
    public async Task<ActionResult<RecipeStepDto>> UpdateRecipeStep(
        int stepId,
        CreateRecipeStepDto recipeStepDto)
    {
        var updatedStep =
            await _stepService.UpdateRecipeStepAsync(
                stepId,
                recipeStepDto);

        if (updatedStep == null)
            return NotFound();

        return Ok(updatedStep);
    }
    [HttpDelete("steps/{stepId}")]
    public async Task<IActionResult> DeleteStep(int stepId)
    {
        var deleted = await _stepService.DeleteRecipeStepAsync(stepId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
    
    [HttpPost("{recipeId}/images")]
    public async Task<ActionResult<RecipeImageDto>> AddImage(
        int recipeId,
        [FromForm] CreateRecipeImageDto imageDto)
    {
        var createdImage =
            await _imageService.AddRecipeImageAsync(recipeId, imageDto);

        if (createdImage == null)
            return NotFound();

        return CreatedAtAction(
            nameof(AddImage),
            new { id = createdImage.Id },
            createdImage);
    }
    [HttpPost("{recipeId}/tags")]
    public async Task<ActionResult<TagDto>> AddTag(int recipeId, CreateTagDto tagDto)
    {
        var createdTag=await _tagService.AddTagToRecipeAsync(recipeId, tagDto);
        if(createdTag == null)
        {
            return NotFound();
        }
        return Ok(createdTag);
        
    }
    [HttpDelete("{recipeId}/tags/{tagId}")]
    public async Task<IActionResult> RemoveTag(int recipeId, int tagId)
    {
        var removed =
        await _tagService.RemoveTagFromRecipeAsync(
            recipeId,
            tagId);

    if (!removed)
    {
        return NotFound();
    }

    return NoContent();
        
    }
}