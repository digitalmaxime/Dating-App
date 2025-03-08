using API.Data;
using API.Interfaces;
using API.MappingProfiles;
using API.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
namespace API.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddAutoMapper(typeof(UserMappingProfile));
        return services;
    }
}
