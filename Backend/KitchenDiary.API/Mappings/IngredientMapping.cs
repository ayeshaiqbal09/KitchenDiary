using KitchenDiary.API.DTOs;
using KitchenDiary.API.Models;

namespace KitchenDiary.API.Mappings;

public static class IngredientMappings
{
    public static IngredientDto ToIngredientDto(this Ingredient ingredient)
    {
        return new IngredientDto
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            Quantity = ingredient.Quantity
        };
    }
}