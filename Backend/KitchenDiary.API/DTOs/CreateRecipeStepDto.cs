using System.ComponentModel.DataAnnotations;

namespace KitchenDiary.API.DTOs;

public class CreateRecipeStepDto
{
    [Range(1, int.MaxValue)]
    public int StepNumber{get; set;}
    [Required]
    [StringLength(10000, MinimumLength = 2)]
    public string Instruction { get; set; } = string.Empty;
}