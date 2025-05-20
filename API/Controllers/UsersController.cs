using System.Runtime.InteropServices.Marshalling;
using System.Security.Claims;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsersController(IUserRepository userRepository, IMapper mapper) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        Thread.Sleep(TimeSpan.FromSeconds(2));
        var users = await userRepository.GetMembersAsync();
        return Ok(users);
    }

    [HttpGet("{username}")] // api/users/lisa
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        Thread.Sleep(TimeSpan.FromSeconds(2));
        var user = await userRepository.GetMemberByUsernameAsync(username);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto dto)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username2 = HttpContext.User.Identity.Name;

        if (username is null) return BadRequest("No username found in token");
        
        var user = await userRepository.GetUntrackedUserByUsernameAsync(username);
        
        if (user == null) return NotFound("User not found");
        
        mapper.Map(dto, user);
        
        // additional delay to simulate production response time
        Thread.Sleep(TimeSpan.FromSeconds(2));
        
        if(await userRepository.Update(user)) return NoContent();
        
        return BadRequest("Failed to update user");
        
    }
}