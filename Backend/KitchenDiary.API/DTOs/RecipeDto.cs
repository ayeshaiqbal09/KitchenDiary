namespace KitchenDiary.API.DTOs;

public class RecipeDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Summary { get; set; }

    public decimal Rating { get; set; }

    public string? Notes { get; set; }

    public DateTime DateAdded { get; set; }
    public List<IngredientDto> Ingredients{get; set;}=[];
}