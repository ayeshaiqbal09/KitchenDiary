namespace KitchenDiary.API.DTOs;

public class CreateRecipeStepDto
{
    public int StepNumber{get; set;}
    public string Instruction { get; set; } = string.Empty;
}