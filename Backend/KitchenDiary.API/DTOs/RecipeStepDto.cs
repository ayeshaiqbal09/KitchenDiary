using System.ComponentModel.DataAnnotations;

public class RecipeStepDto{
    public int Id { get; set; }

    public int StepNumber { get; set; }

    public string Instruction { get; set; } = string.Empty;
}