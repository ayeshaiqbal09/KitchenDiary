using System.ComponentModel.DataAnnotations;

public class CreateIngredientDto
{
    
    [MaxLength(300)]
    public string Name{get; set;} = string.Empty;

    [MaxLength(30)]
    public string? Quantity {get; set; }
}