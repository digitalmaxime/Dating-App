using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(DataContext context, ITokenService tokenService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        using var hmac = new HMACSHA512();

        var user = new AppUser() {
            UserName = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        if(await AlreadyExistingUsername(registerDto.Username)) {
            return BadRequest("username already in use");
        }

        context.Users.Add(user);
        await context.SaveChangesAsync();

        var token = tokenService.CreateToken(user);
        return new UserDto(user.UserName, token);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) {
        var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
        
        if (user == null) return NotFound();

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        // if (computedPasswordHash != user.PasswordHash) return BadRequest("invalid password");

        for(int i = 0; i < computedPasswordHash.Length; i++) {
            if (user.PasswordHash[i] != computedPasswordHash[i]) {
                return Unauthorized("invalid password");
            }
        }

        var token = tokenService.CreateToken(user);

        return new UserDto(user.UserName, token);
    }


    private async Task<bool> AlreadyExistingUsername(string username) {
        return await context.Users.AnyAsync(u => u.UserName.ToLower() == username.ToLower());
    }
}
