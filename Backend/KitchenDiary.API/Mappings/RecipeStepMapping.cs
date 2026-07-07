using KitchenDiary.API.DTOs;
using KitchenDiary.API.Models;

namespace KitchenDiary.API.Mappings;

public static class RecipeStepMappings
{
    public static RecipeStepDto ToRecipeStepDto(this RecipeStep step)
    {
        return new RecipeStepDto
        {
            Id = step.Id,
            StepNumber = step.StepNumber,
            Instruction = step.Instruction
        };
    }
}