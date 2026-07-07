using KitchenDiary.API.DTOs;
using KitchenDiary.API.Models;

namespace KitchenDiary.API.Mappings;

public static class TagMappings
{
    public static TagDto ToTagDto(this Tag tag)
    {
        return new TagDto
        {
            Id = tag.Id,
            Name = tag.Name
        };
    }
}