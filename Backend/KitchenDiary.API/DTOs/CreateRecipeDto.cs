namespace KitchenDiary.API.DTOs;

public class CreateRecipeDto
{
    public string Title { get; set; } = string.Empty;

    public string? Summary { get; set; }

    public decimal Rating { get; set; }

    public string? Notes { get; set; }
}