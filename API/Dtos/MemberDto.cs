using API.Entities;
using api.Extensions;

namespace API.Dtos;

public class AppUserDto
{
    public required string UserName { get; set; }
    public int Age { get; set; }
    public required string Gender { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public string? PhotoUrl { get; set; }
}