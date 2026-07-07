using KitchenDiary.API.DTOs;
using KitchenDiary.API.Models;

namespace KitchenDiary.API.Mappings;

public static class RecipeMappings
{
    public static RecipeDto ToRecipeDto(this Recipe recipe)
{
    return new RecipeDto
    {
        Id = recipe.Id,
        Title = recipe.Title,
        Summary = recipe.Summary,
        Rating = recipe.Rating,
        Notes = recipe.Notes,
        DateAdded = recipe.DateAdded,

        Ingredients = recipe.Ingredients
        .Select(i => i.ToIngredientDto())
        .ToList(),

        Steps = recipe.Steps
        .Select(s => s.ToRecipeStepDto())
        .ToList(),

        Images = recipe.Images
            .Select(i => i.ToRecipeImageDto())
            .ToList(),

        Tags = recipe.RecipeTags
            .Select(rt => rt.Tag.ToTagDto())
            .ToList()
    };
}
    public static Recipe ToRecipe(this CreateRecipeDto dto)
    {
        return new Recipe
        {
            Title = dto.Title,
            Summary = dto.Summary,
            Rating = dto.Rating,
            Notes = dto.Notes,
            DateAdded = DateTime.UtcNow
        };
    }
    public static void UpdateRecipe(
    this Recipe recipe,
    CreateRecipeDto dto)
    {
        recipe.Title = dto.Title;
        recipe.Summary = dto.Summary;
        recipe.Rating = dto.Rating;
        recipe.Notes = dto.Notes;
    }

}