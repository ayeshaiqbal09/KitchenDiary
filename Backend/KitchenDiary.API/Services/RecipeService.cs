using KitchenDiary.API.Data;
using KitchenDiary.API.DTOs;
using KitchenDiary.API.Interfaces;
using KitchenDiary.API.Models;

namespace KitchenDiary.API.Services;

public class RecipeService : IRecipeService
{
    private readonly ApplicationDbContext _context;

    public RecipeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RecipeDto> CreateRecipeAsync(CreateRecipeDto recipeDto)
    {
        var recipe = new Recipe
        {
            Title = recipeDto.Title,
            Summary = recipeDto.Summary,
            Rating = recipeDto.Rating,
            Notes = recipeDto.Notes,
            DateAdded = DateTime.UtcNow
        };

        _context.Recipes.Add(recipe);

        await _context.SaveChangesAsync();

        return new RecipeDto
        {
            Id = recipe.Id,
            Title = recipe.Title,
            Summary = recipe.Summary,
            Rating = recipe.Rating,
            Notes = recipe.Notes,
            DateAdded = recipe.DateAdded
        };
    }
}