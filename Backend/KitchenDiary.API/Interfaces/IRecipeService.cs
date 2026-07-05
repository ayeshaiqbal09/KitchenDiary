using KitchenDiary.API.DTOs;

namespace KitchenDiary.API.Interfaces;

public interface IRecipeService
{
    Task<RecipeDto> CreateRecipeAsync(CreateRecipeDto recipeDto);
    Task<IEnumerable<RecipeDto>> GetAllRecipesAsync();
    Task<RecipeDto?> GetRecipeByIdAsync(int id);
    Task<RecipeDto?> UpdateRecipeAsync(int id, CreateRecipeDto recipeDto);
    Task<bool> DeleteRecipeAsync(int id);
    Task<IEnumerable<RecipeDto>> SearchRecipesAsync(string searchTerm);
}