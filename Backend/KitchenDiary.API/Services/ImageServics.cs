using KitchenDiary.API.Data;
using KitchenDiary.API.DTOs;
using KitchenDiary.API.Interfaces;
using KitchenDiary.API.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenDiary.API.Services;

public class ImageService : IImageService
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<RecipeService> _logger;
    private readonly CloudinaryService _cloudinaryService;
    public ImageService(ApplicationDbContext context, IWebHostEnvironment environment, ILogger<RecipeService> logger, CloudinaryService cloudinaryService)
    {
        _context = context;
        _logger = logger;
        _environment=environment;
        _cloudinaryService = cloudinaryService;
    }
    public async Task<RecipeImageDto?> AddRecipeImageAsync(int recipeId,
    CreateRecipeImageDto imageDto)
{
    var recipe = await _context.Recipes.FindAsync(recipeId);

    if (recipe == null)
    {
        _logger.LogWarning("Recipe {RecipeId} not found.", recipeId);
        return null;
    }
    if (imageDto.Image == null || imageDto.Image.Length == 0)
    {
        return null;
    }
    const long maxFileSize = 5 * 1024 * 1024;
    if (imageDto.Image.Length > maxFileSize)
    {
        _logger.LogWarning("Image upload rejected because the file exceeded the maximum size.");

        return null;
    }
    var allowedExtensions = new[]
    {
        ".jpg",
        ".jpeg",
        ".png"
    };
    
    var extension =Path.GetExtension(imageDto.Image.FileName).ToLowerInvariant();
    if (!allowedExtensions.Contains(extension))
    {
        _logger.LogWarning("Image upload rejected because of invalid file type.");

        return null;
    }
    // var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageDto.Image.FileName);

    // var webRoot = _environment.WebRootPath;
    
    // var uploadsFolder = Path.Combine(webRoot, "uploads", "recipes");
    
    // Directory.CreateDirectory(uploadsFolder);
    
    // var filePath = Path.Combine( uploadsFolder, fileName);
    
    // using var stream = new FileStream(filePath, FileMode.Create);

    // await imageDto.Image.CopyToAsync(stream);
    var imageUrl = await _cloudinaryService.UploadImageAsync(imageDto.Image);

    var recipeImage = new RecipeImage
    {
         ImagePath = imageUrl,

        RecipeId = recipeId
    };
    _context.RecipeImages.Add(recipeImage);

    await _context.SaveChangesAsync();
    _logger.LogInformation("Image for {RecipeId} added successfully.",
        recipe.Id);
    return new RecipeImageDto
    {
        Id = recipeImage.Id,
        ImagePath = recipeImage.ImagePath
    };
    
}
public async Task<bool> DeleteRecipeImageAsync(int imageId)
{
    var image = await _context.RecipeImages.FindAsync(imageId);

    if (image == null)
        return false;

    // var filePath = Path.Combine(
    //     _environment.ContentRootPath,
    //     image.ImagePath);

    // if (File.Exists(filePath))
    // {
    //     File.Delete(filePath);
    // }

    _context.RecipeImages.Remove(image);

    await _context.SaveChangesAsync();

    return true;
}
public async Task<bool> SetCoverImageAsync(
    int recipeId,
    int imageId)
{
    var recipe = await _context.Recipes
        .Include(r => r.Images)
        .FirstOrDefaultAsync(r => r.Id == recipeId);

    if (recipe == null)
        return false;

    foreach (var image in recipe.Images)
    {
        image.IsCoverImage = false;
    }

    var selected =
        recipe.Images.FirstOrDefault(i => i.Id == imageId);

    if (selected == null)
        return false;

    selected.IsCoverImage = true;

    await _context.SaveChangesAsync();

    return true;
}
}