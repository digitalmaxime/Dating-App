using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

        return services;
    }
}
