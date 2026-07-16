using KitchenDiary.API.DTOs;
using KitchenDiary.API.Models;

namespace KitchenDiary.API.Mappings;

public static class ImageMappings
{
    public static RecipeImageDto ToRecipeImageDto(this RecipeImage image)
    {
        return new RecipeImageDto
        {
            Id = image.Id,
            ImagePath = image.ImagePath,
            IsCoverImage = image.IsCoverImage
        };
    }
}