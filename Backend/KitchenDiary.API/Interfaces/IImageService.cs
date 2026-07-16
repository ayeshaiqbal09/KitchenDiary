using KitchenDiary.API.DTOs;

namespace KitchenDiary.API.Interfaces;

public interface IImageService
{
    Task<RecipeImageDto?> AddRecipeImageAsync(int recipeId, CreateRecipeImageDto imageDto);
    Task<bool> DeleteRecipeImageAsync(int imageId);
    Task<bool> SetCoverImageAsync(
    int recipeId,
    int imageId);
}