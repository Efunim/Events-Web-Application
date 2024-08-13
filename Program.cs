using Events.Application.Interfaces.Repositories;
using Events.Application.Interfaces.Services;
using Events.Application.Services;
using Events.Infastructure.Repositories;
using Events_Web_application.Infrastructure;
using System.Reflection;
using FluentValidation;
using Events.Infastructure.Authentification;
using Events.Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using Events.Application.DTO;
using Events.Infastructure.Data.Files;
using Microsoft.AspNetCore.Authorization;
using Events.Infastructure.Authentification.Policies;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<ApplicationContext>(options => 
    { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });

builder.Services.AddAutoMapper(typeof(DataMappingProfile));

builder.Services.AddSingleton<IAuthorizationHandler, MinimumAgeRequirementHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AtLeast12", policy =>
        policy.Requirements.Add(new MinimumAgeRequirement(12)));
});

builder.Services.AddControllers();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEventCategoryService, EventCategoryService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthentificationService, AuthentificationService>();

builder.Services.AddScoped<IFileManager, FileManager>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler();
app.UseMiddleware<JwtMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Events API v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapGet("/exception", () =>
{
    throw new ArgumentNullException("There is nothing. Literally");
});

app.MapControllers();

app.Run();
