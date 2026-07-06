using KitchenDiary.API.Data;
using KitchenDiary.API.DTOs;
using KitchenDiary.API.Interfaces;
using KitchenDiary.API.Models;
using Microsoft.EntityFrameworkCore;
namespace KitchenDiary.API.Services;

public class TagService : ITagService
{
    private readonly ApplicationDbContext _context;
    public TagService(ApplicationDbContext context)
    {
        _context=context;
    }

    public async Task<TagDto?> AddTagToRecipeAsync(int recipeId, CreateTagDto tagDto)
    {
        var recipe = await _context.Recipes.FindAsync(recipeId);
        if(recipe == null)
        {
            return null;
        }
        var tag = await _context.Tags.FirstOrDefaultAsync(
            t => t.Name.ToLower() == tagDto.Name.ToLower());
        
        if(tag == null)
        {
            tag = new Tag
            {
                Name=tagDto.Name.Trim().ToLower()
            };
            _context.Tags.Add(tag);

            await _context.SaveChangesAsync();
        }
        var alreadyTagged = await _context.RecipeTags.AnyAsync( rt => rt.RecipeId == recipeId &&
          rt.TagId == tag.Id);

          if (alreadyTagged)
        {
            return new TagDto
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }
        var recipeTag = new RecipeTag
        {
            RecipeId = recipeId,
            TagId = tag.Id
        };
        _context.RecipeTags.Add(recipeTag);

        await _context.SaveChangesAsync();
        return new TagDto
        {
            Id = tag.Id,
            Name = tag.Name
        };
    }

    public async Task<bool> RemoveTagFromRecipeAsync(int recipeId, int tagId)
    {
        
        var recipeTag = await _context.RecipeTags.FirstOrDefaultAsync(
        rt => rt.RecipeId == recipeId &&
            rt.TagId == tagId);

    if (recipeTag == null)
    {
        return false;
    }

    _context.RecipeTags.Remove(recipeTag);

    await _context.SaveChangesAsync();

    return true;

    }
}