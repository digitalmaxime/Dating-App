using API;
using API.Data;
using API.Extensions;
using API.Middleware;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddControllers();
// builder.Services.AddScoped<IValidator<RegisterDto>, RegisterDtoValidator>();
builder.Services.AddValidatorsFromAssembly(typeof(ApiLibrary).Assembly);

builder.Services.ConfigureSecurity(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);
builder.Services.RegisterInfrastructure(builder.Configuration);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    try
    {
        // Create a scoped service provider to resolve dependencies
        using var scope = app.Services.CreateScope();

        // resolve the logger service
        var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        await dataContext.Database.MigrateAsync();
        await Seed.SeedData(dataContext);
    }
    catch (Exception e)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "Error during database seed" + e.Message);
    }
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(o =>
{
    o.AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("https://localhost:4200", "http://localhost:4200", "*:4200");
});

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();