using System.ComponentModel.DataAnnotations;

namespace KitchenDiary.API.DTOs;

public class CreateRecipeDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Title { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Summary { get; set; }

    [Range(0, 5)]
    public decimal Rating { get; set; }

    [StringLength(5000)]
    public string? Notes { get; set; }
}