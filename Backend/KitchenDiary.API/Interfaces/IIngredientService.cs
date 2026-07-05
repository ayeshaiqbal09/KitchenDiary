using KitchenDiary.API.DTOs;

namespace KitchenDiary.API.Interfaces;

public interface IIngredientService
{
    Task<IngredientDto?> AddIngredientAsync( int recipeId,
     CreateIngredientDto ingredientDto);
    
    Task<IngredientDto?> UpdateIngredientAsync(
        int ingredientId, CreateIngredientDto ingredientDto);
    Task<bool> DeleteIngredientAsync(int ingredientId);
}