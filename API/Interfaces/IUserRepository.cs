using API.Data;
using API.Dtos;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
    public Task Update(AppUser user);
    public Task<bool> SaveAllAsync();
    public Task<IEnumerable<AppUser>> GetUsersAsync();
    public Task<AppUser?> GetUserByIdAsync(int id);
    public Task<AppUser?> GetUserByUsernameAsync(string username);
    
    public Task<IEnumerable<MemberDto>> GetMembersAsync();
    public Task<MemberDto?> GetMemberByUsernameAsync(string username);
    public bool Delete(AppUser user);
}