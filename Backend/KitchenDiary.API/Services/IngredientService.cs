using KitchenDiary.API.Data;
using KitchenDiary.API.DTOs;
using KitchenDiary.API.Interfaces;
using KitchenDiary.API.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenDiary.API.Services;

public class IngredientService : IIngredientService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<RecipeService> _logger;
    public IngredientService(ApplicationDbContext context, ILogger<RecipeService> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<IngredientDto?> AddIngredientAsync(int recipeId, CreateIngredientDto ingredientDto)
    {
        var recipe= await _context.Recipes.FindAsync(recipeId);
        if (recipe == null)
        {
            _logger.LogWarning("Recipe {RecipeId} not found.", recipeId);
            return null;
        }
       
        var ingredient = new Ingredient{
            Name=ingredientDto.Name,
            Quantity=ingredientDto.Quantity,
            RecipeId=recipeId
        };
        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Ingredients for {RecipeId} added successfully.",
        recipe.Id);

        return new IngredientDto
        {
            Id=ingredient.Id,
            Name=ingredient.Name,
            Quantity=ingredient.Quantity
        };

    }
    public async Task<IngredientDto?> UpdateIngredientAsync(int ingredientId, CreateIngredientDto ingredientDto)
    {
        var ingredient=await _context.Ingredients.FindAsync(ingredientId);
        if(ingredient==null)
        {
         _logger.LogWarning("Ingredient for {RecipeId} not found.", ingredientId);

            return null;
        }
        ingredient.Name=ingredientDto.Name;
        ingredient.Quantity=ingredientDto.Quantity;
        await _context.SaveChangesAsync();
        _logger.LogInformation("Ingredients for {ingredientId} updated successfully.",
        ingredientId);
        return new IngredientDto
        {
            Id=ingredient.Id,
            Name=ingredientDto.Name,
            Quantity=ingredientDto.Quantity
        };
    }
    public async Task<bool> DeleteIngredientAsync(int ingredientId)
    {
        var ingredient= await _context.Ingredients.FindAsync(ingredientId);
        if(ingredient==null)
        {
            return false;
        }
        _context.Ingredients.Remove(ingredient);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Ingredients for {ingredientId} deleted successfully.",
        ingredientId);
        return true;
    }
}