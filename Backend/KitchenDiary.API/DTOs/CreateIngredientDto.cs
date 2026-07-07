using System.ComponentModel.DataAnnotations;

public class CreateIngredientDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name{get; set;} = string.Empty;

    [MaxLength(50)]
    public string? Quantity {get; set; }
}