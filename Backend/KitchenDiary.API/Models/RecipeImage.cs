namespace KitchenDiary.API.Models;

public class RecipeImage
{
    public int Id { get; set; }

    public string ImagePath { get; set; } = string.Empty;

    public int RecipeId { get; set; }

    public Recipe Recipe { get; set; } = null!;
}