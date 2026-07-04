using KitchenDiary.API.DTOs;

namespace KitchenDiary.API.Interfaces;

public interface IRecipeService
{
    Task<RecipeDto> CreateRecipeAsync(CreateRecipeDto recipeDto);
}