using System.Data;
using System.Security.Cryptography;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Data;

public class Seed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.Users.Any()) return;
        
        var usersJson = File.ReadAllText("./Data/UserSeedData.json");
        
        var option = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        var users = JsonSerializer.Deserialize<List<AppUser>>(usersJson, option);
        
        if (users == null) throw new DataException("Users not found while seeding");

        foreach (var user in users)
        {
            using var hmac = new HMACSHA512();
            
            user.UserName = user.UserName.ToLower();
            user.PasswordHash = hmac.ComputeHash("Pa$$w0rd"u8.ToArray());
            user.PasswordSalt = hmac.Key;
        }
        
        await context.Users.AddRangeAsync(users!);
        
        await context.SaveChangesAsync();
    }
}