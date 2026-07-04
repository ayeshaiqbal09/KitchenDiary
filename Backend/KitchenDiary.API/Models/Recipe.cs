namespace KitchenDiary.API.Models;

public class Recipe
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Summary { get; set; }

    public decimal  Rating { get; set; }

    public string? Notes { get; set; }

    public DateTime DateAdded { get; set; }

    public ICollection<Ingredient> Ingredients { get; set; } = [];

    public ICollection<RecipeStep> Steps { get; set; } = [];

    public ICollection<RecipeImage> Images { get; set; } = [];

    public ICollection<RecipeTag> RecipeTags { get; set; } = [];
}