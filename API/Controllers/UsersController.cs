using System.Runtime.InteropServices.Marshalling;
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
        var users = await userRepository.GetMembersAsync();

        return Ok(users);
    }

    [HttpGet("{username}")] // api/users/lisa
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        var user = await userRepository.GetMemberByUsernameAsync(username);

        return user == null ? NotFound() : Ok(user);
    }
}