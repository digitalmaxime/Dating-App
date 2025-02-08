using API.Interfaces;
using API.Services;
namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration) {
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }
}
