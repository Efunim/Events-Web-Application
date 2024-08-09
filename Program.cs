using Events.Application.Interfaces.Repositories;
using Events.Application.Interfaces.Services;
using Events.Application.Services;
using Events.Infastructure.Repositories;
using Events_Web_application.Infrastructure;
using System.Reflection;
using FluentValidation;
using Events.Infastructure.Authentification;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEventCategoryService, EventCategoryService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler();
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapGet("/exception", () =>
{
    throw new ArgumentNullException("There is nothing. Literally");
});
app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
