using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController(DataContext dataContext) : ControllerBase
{

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await dataContext.Users.ToListAsync();

        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        var user = await dataContext.Users.FindAsync(id);

        return user == null ? NotFound() : Ok(user);
    }
}
