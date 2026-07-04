namespace KitchenDiary.API.Models;

public class Ingredient
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int RecipeId { get; set; }

    public Recipe Recipe { get; set; } = null!;
}