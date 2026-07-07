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
    public List<RecipeStepDto> Steps { get; set; } = [];

    public List<RecipeImageDto> Images { get; set; } = [];

    public List<TagDto> Tags { get; set; } = [];
}