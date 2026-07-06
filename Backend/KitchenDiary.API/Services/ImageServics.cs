using KitchenDiary.API.Data;
using KitchenDiary.API.DTOs;
using KitchenDiary.API.Interfaces;
using KitchenDiary.API.Models;


namespace KitchenDiary.API.Services;

public class ImageService : IImageService
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;
    public ImageService(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context=context;
        _environment=environment;
    }
    public async Task<RecipeImageDto?> AddRecipeImageAsync(int recipeId,
    CreateRecipeImageDto imageDto)
{
    var recipe = await _context.Recipes.FindAsync(recipeId);

    if (recipe == null)
        return null;
    if (imageDto.Image == null || imageDto.Image.Length == 0)
    {
        return null;
    }
    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageDto.Image.FileName);
    
    var webRoot = _environment.WebRootPath;
    
    var uploadsFolder = Path.Combine(webRoot, "uploads", "recipes");
    
    Directory.CreateDirectory(uploadsFolder);
    
    var filePath = Path.Combine( uploadsFolder, fileName);
    
    using var stream = new FileStream(filePath, FileMode.Create);

    await imageDto.Image.CopyToAsync(stream);

    var recipeImage = new RecipeImage
    {
        ImagePath = "uploads/recipes/" + fileName,

        RecipeId = recipeId
    };
    _context.RecipeImages.Add(recipeImage);

    await _context.SaveChangesAsync();
    return new RecipeImageDto
    {
        Id = recipeImage.Id,
        ImagePath = recipeImage.ImagePath
    };
    
}
}