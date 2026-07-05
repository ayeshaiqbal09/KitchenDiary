using KitchenDiary.API.Data;
using KitchenDiary.API.DTOs;
using KitchenDiary.API.Interfaces;
using KitchenDiary.API.Models;
using Microsoft.EntityFrameworkCore;
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
        var recipe = new Recipe();
        MapToRecipe(recipeDto, recipe);
        recipe.DateAdded= DateTime.UtcNow;

        _context.Recipes.Add(recipe);

        await _context.SaveChangesAsync();

        return MapToRecipeDto(recipe);
    }
    public async Task<IEnumerable<RecipeDto>> GetAllRecipesAsync()
    {
        var recipes = await _context.Recipes.ToListAsync();

        return recipes.Select(MapToRecipeDto);
    }
    public async Task<RecipeDto?> GetRecipeByIdAsync(int id)
    {
        var recipe = await _context.Recipes.Include(r=> r.Ingredients).
        FirstOrDefaultAsync(r => r.Id==id);

        if (recipe == null)
            return null;

        return new RecipeDto
        {
            Id = recipe.Id,
            Title = recipe.Title,
            Summary = recipe.Summary,
            Rating = recipe.Rating,
            Notes = recipe.Notes,
            DateAdded = recipe.DateAdded,

            Ingredients = recipe.Ingredients
                .Select(i => new IngredientDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Quantity = i.Quantity
                })
                .ToList()
        };
    }
    public async Task<RecipeDto?> UpdateRecipeAsync(int id, CreateRecipeDto recipeDto)
    {
        var recipe = await _context.Recipes.FindAsync(id);

        if (recipe == null)
            return null;

        MapToRecipe(recipeDto, recipe);

        await _context.SaveChangesAsync();

        return MapToRecipeDto(recipe);
    }
    public async Task<bool> DeleteRecipeAsync(int id)
    {
        var recipe = await _context.Recipes.FindAsync(id);

        if (recipe == null)
            return false;

        _context.Recipes.Remove(recipe);

        await _context.SaveChangesAsync();

        return true;
    }
    public async Task<IEnumerable<RecipeDto>> SearchRecipesAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllRecipesAsync();

        var recipes = await _context.Recipes
                .Where(recipe => recipe.Title.ToLower().Contains(searchTerm.ToLower())).ToListAsync();

        return recipes.Select(MapToRecipeDto);
    }
    //Helper Mthods
    private static RecipeDto MapToRecipeDto(Recipe recipe)
    {
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
    private static void MapToRecipe(CreateRecipeDto dto, Recipe recipe)
    {
        recipe.Title = dto.Title;
        recipe.Summary = dto.Summary;
        recipe.Rating = dto.Rating;
        recipe.Notes = dto.Notes;
    }
}