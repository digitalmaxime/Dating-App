using API.Extensions;
using API.Middleware;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddControllers();
// builder.Services.AddScoped<IValidator<RegisterDto>, RegisterDtoValidator>();
builder.Services.AddValidatorsFromAssembly(typeof(ApiLibrary).Assembly);

builder.Services.ConfigureSecurity(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);
builder.Services.RegisterInfrastructure(builder.Configuration);
var app = builder.Build();

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
