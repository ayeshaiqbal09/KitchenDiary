using KitchenDiary.API.DTOs;

namespace KitchenDiary.API.Interfaces;

public interface IImageService
{
    Task<RecipeImageDto?> AddRecipeImageAsync(int recipeId, CreateRecipeImageDto imageDto);
}