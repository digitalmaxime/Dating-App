using API.Data;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using API.MappingProfiles;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
{
    public Task SetEntryStateModified(AppUser user)
    {
        context.Entry(user).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public async Task<bool> SaveAllAsync()
    {
        return  await context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await context.Users
            .Include(x => x.Photos)
            .ToListAsync();
    }

    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<AppUser?> GetUntrackedUserByUsernameAsync(string username)
    {
        return await context.Users
            .AsNoTracking()
            .Include(x => x.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
        return await context.Users
            .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<MemberDto?> GetMemberByUsernameAsync(string username)
    {
        return await context.Users
            .Where(x => x.UserName == username)
            .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.UserName == username);
    }

    public bool Delete(AppUser user)
    {
        context.Users.Remove(user);
        return context.SaveChanges() > 0;
    }
    
    public async Task<bool> Update(AppUser user)
    {
        context.Update(user);
        return  await context.SaveChangesAsync() > 0;
    }
}