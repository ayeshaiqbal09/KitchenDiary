using Microsoft.AspNetCore.Identity;

namespace KitchenDiary.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}