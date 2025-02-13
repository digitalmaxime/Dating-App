using System.Reflection;
using API.Dtos;
using API.Extensions;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IValidator<RegisterDto>, RegisterDtoValidator>();
builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(ApiLibrary)));

builder.Services.ConfigureSecurity(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);
builder.Services.RegisterInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200", "http://localhost:4200"));

app.MapControllers();

app.Run();
