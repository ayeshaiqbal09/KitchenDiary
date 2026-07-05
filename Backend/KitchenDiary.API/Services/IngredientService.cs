using KitchenDiary.API.Data;
using KitchenDiary.API.DTOs;
using KitchenDiary.API.Interfaces;
using KitchenDiary.API.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenDiary.API.Services;

public class IngredientService : IIngredientService
{
    private readonly ApplicationDbContext _context;
    public IngredientService(ApplicationDbContext context)
    {
        _context=context;
    }
    public async Task<IngredientDto?> AddIngredientAsync(int recipeId, CreateIngredientDto ingredientDto)
    {
        var recipe= await _context.Recipes.FindAsync(recipeId);
        if(recipe==null)
        {
            return null;
        }
       
        var ingredient = new Ingredient{
            Name=ingredientDto.Name,
            Quantity=ingredientDto.Quantity,
            RecipeId=recipeId
        };
        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();

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
            return null;
        }
        ingredient.Name=ingredientDto.Name;
        ingredient.Quantity=ingredientDto.Quantity;
        await _context.SaveChangesAsync();
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
        return true;
    }
}