using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using KitchenDiary.API.Configurations;
using Microsoft.Extensions.Options;

namespace KitchenDiary.API.Services;

public class CloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> settings)
    {
        var account = new Account(
            settings.Value.CloudName,
            settings.Value.ApiKey,
            settings.Value.ApiSecret);

        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file.Length == 0)
            throw new Exception("Empty file.");

        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = "KitchenDiary"
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error != null)
            throw new Exception(uploadResult.Error.Message);

        return uploadResult.SecureUrl.ToString();
    }
}