namespace KitchenDiary.API.DTOs;

public class RecipeImageDto
{
    public int Id { get; set; }
    public string ImagePath { get; set; } = string.Empty;

     public bool IsCoverImage { get; set; }
}