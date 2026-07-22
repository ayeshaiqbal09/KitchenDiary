using KitchenDiary.API.Models;

namespace KitchenDiary.API.Interfaces;

public interface ITokenService
{
    Task<string> CreateTokenAsync(ApplicationUser user);
}