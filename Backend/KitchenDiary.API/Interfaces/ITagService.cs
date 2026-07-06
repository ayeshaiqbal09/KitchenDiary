using KitchenDiary.API.DTOs;

namespace KitchenDiary.API.Interfaces;

public interface ITagService
{
    Task<TagDto?> AddTagToRecipeAsync(int recipeId, CreateTagDto tagDto);
    Task<bool> RemoveTagFromRecipeAsync(int recipeId, int tagId);
}
