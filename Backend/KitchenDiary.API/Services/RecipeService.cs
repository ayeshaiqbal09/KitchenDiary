using KitchenDiary.API.Data;
using KitchenDiary.API.DTOs;
using KitchenDiary.API.Interfaces;
using KitchenDiary.API.Mappings;
using KitchenDiary.API.Models;
using Microsoft.EntityFrameworkCore;
namespace KitchenDiary.API.Services;

public class RecipeService : IRecipeService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<RecipeService> _logger;
    private readonly IWebHostEnvironment _environment;

    public RecipeService(ApplicationDbContext context, ILogger<RecipeService> logger, IWebHostEnvironment environment)
    {
        _context = context;
        _logger = logger;
        _environment=environment;
    }

    public async Task<RecipeDto> CreateRecipeAsync(CreateRecipeDto recipeDto)
    {
        var recipe = recipeDto.ToRecipe();
        
        _context.Recipes.Add(recipe);
        var existingTags = await _context.Tags.ToListAsync();

foreach (var tagName in recipeDto.Tags)
{
    if (string.IsNullOrWhiteSpace(tagName))
        continue;

   var trimmedTag = tagName.Trim();

var normalizedTag =
    char.ToUpper(trimmedTag[0]) +
    trimmedTag.Substring(1).ToLower();
    var tag = existingTags.FirstOrDefault(t =>
        t.Name.Equals(normalizedTag, StringComparison.OrdinalIgnoreCase));

    if (tag == null)
    {
        tag = new Tag
        {
            Name = normalizedTag
        };

        _context.Tags.Add(tag);
        existingTags.Add(tag);
    }

    recipe.RecipeTags.Add(new RecipeTag
    {
        Recipe = recipe,
        Tag = tag
    });
}

        await _context.SaveChangesAsync();
        _logger.LogInformation("Recipe {RecipeId} created successfully.",
        recipe.Id);

        return recipe.ToRecipeDto();
    }
    public async Task<IEnumerable<RecipeDto>> GetAllRecipesAsync()
    {
        var recipes = await _context.Recipes
        .Include(r => r.Ingredients)
        .Include(r => r.Steps)
        .Include(r => r.Images)
        .Include(r => r.RecipeTags)
            .ThenInclude(rt => rt.Tag)
        .ToListAsync();

        return recipes.Select(r => r.ToRecipeDto());
    }
    public async Task<RecipeDto?> GetRecipeByIdAsync(int id)
    {
        var recipe = await _context.Recipes
        .Include(r => r.Ingredients)
        .Include(r => r.Steps)
        .Include(r => r.Images)
        .Include(r => r.RecipeTags)
            .ThenInclude(rt => rt.Tag)
        .FirstOrDefaultAsync(r => r.Id == id);

        if (recipe == null)
        {
            _logger.LogWarning("Recipe {RecipeId} not found.", id);

            return null;
        }

        return recipe.ToRecipeDto();
    }
    public async Task<RecipeDto?> UpdateRecipeAsync(int id, CreateRecipeDto recipeDto)
    {
        var recipe = await _context.Recipes
    .Include(r => r.Ingredients)
    .Include(r => r.Steps)
    .Include(r => r.RecipeTags)
        .ThenInclude(rt => rt.Tag)
    .FirstOrDefaultAsync(r => r.Id == id);

        if (recipe == null)
        {
            _logger.LogWarning("Recipe {RecipeId} not found.", id);
            return null;
        }
        recipe.Title = recipeDto.Title;
recipe.Summary = recipeDto.Summary;
recipe.Rating = recipeDto.Rating;
recipe.Notes = recipeDto.Notes;
recipe.Ingredients.Clear();

foreach (var ingredient in recipeDto.Ingredients)
{
    if (!string.IsNullOrWhiteSpace(ingredient.Name))
    {
        recipe.Ingredients.Add(new Ingredient
        {
            Name = ingredient.Name.Trim(),
            Quantity = ingredient.Quantity
        });
    }
}
recipe.Steps.Clear();

int stepNumber = 1;

foreach (var step in recipeDto.Steps)
{
    if (!string.IsNullOrWhiteSpace(step))
    {
        recipe.Steps.Add(new RecipeStep
        {
            StepNumber = stepNumber++,
            Instruction = step.Trim()
        });
    }
}
recipe.RecipeTags.Clear();

var existingTags = await _context.Tags.ToListAsync();

foreach (var tagName in recipeDto.Tags)
{
    if (string.IsNullOrWhiteSpace(tagName))
        continue;

    var trimmedTag = tagName.Trim();
    var normalizedTag =
    char.ToUpper(trimmedTag[0]) +
    trimmedTag.Substring(1).ToLower();
    var tag = existingTags
        .FirstOrDefault(t => t.Name.Equals(normalizedTag, StringComparison.OrdinalIgnoreCase));

    if (tag == null)
    {
        tag = new Tag
        {
            Name = normalizedTag
        };

        _context.Tags.Add(tag);
        existingTags.Add(tag);
    }

    recipe.RecipeTags.Add(new RecipeTag
    {
        Recipe = recipe,
        Tag = tag
    });
}
_logger.LogInformation("RecipeTags Count: {Count}", recipe.RecipeTags.Count);

        recipe = await _context.Recipes
    .Include(r => r.Ingredients)
    .Include(r => r.Steps)
    .Include(r => r.Images)
    .Include(r => r.RecipeTags)
        .ThenInclude(rt => rt.Tag)
    .FirstAsync(r => r.Id == id);
_logger.LogInformation("Recipe {RecipeId} updated successfully.",
        recipe.Id);
return recipe.ToRecipeDto();

        

    
    }
    public async Task<bool> DeleteRecipeAsync(int id)
    {
        var recipe = await _context.Recipes.Include(r => r.Images).FirstOrDefaultAsync(r => r.Id == id);
        
        
        if (recipe == null)
        {
            _logger.LogWarning("Recipe {RecipeId} not found.", id);
            return false;
        }
        foreach (var image in recipe.Images)
        {
            var imagePath =Path.Combine( _environment.WebRootPath,
            image.ImagePath);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

        }
        _context.Recipes.Remove(recipe);

        await _context.SaveChangesAsync();

        _logger.LogInformation("Recipe {RecipeId} deleted successfully.", id);

        return true;
    }
    public async Task<IEnumerable<RecipeDto>> SearchRecipesAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllRecipesAsync();

        var recipes = await _context.Recipes
                .Where(recipe => recipe.Title.ToLower().Contains(searchTerm.ToLower())).ToListAsync();

        return recipes.Select(r => r.ToRecipeDto());
    }
    
}