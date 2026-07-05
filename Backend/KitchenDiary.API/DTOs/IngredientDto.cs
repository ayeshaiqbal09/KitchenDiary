using System.ComponentModel.DataAnnotations;

namespace KitchenDiary.API.DTOs;
public class IngredientDto{
    public int Id { get; set; }

    [MaxLength(300)]
    public string Name {get; set; }= string.Empty;
    public string? Quantity {get; set; }
}