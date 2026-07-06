using KitchenDiary.API.DTOs;

namespace KitchenDiary.API.Interfaces;

public interface IRecipeStepService
{
    Task<RecipeStepDto?> AddRecipeStepAsync(int recipeId, CreateRecipeStepDto recipeStepDto);
    Task<RecipeStepDto?> UpdateRecipeStepAsync(int recipeid, CreateRecipeStepDto recipeStepDto);
    Task<bool> DeleteRecipeStepAsync(int stepId);
}
