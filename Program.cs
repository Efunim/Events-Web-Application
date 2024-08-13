using Events.Application.DTO;
using Events.Application.Interfaces.Repositories;
using Events.Application.Interfaces.Services;
using Events.Application.Services;
using Events.Infastructure.Authentification;
using Events.Infastructure.Authentification.Policies;
using Events.Infastructure.Data;
using Events.Infastructure.Data.Files;
using Events.Infastructure.Repositories;
using Events_Web_application.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<ApplicationContext>(options =>
    { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });

builder.Services.AddAutoMapper(typeof(DataMappingProfile));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = AuthConfiguration.ISSUER,
        ValidAudience = AuthConfiguration.AUDIENCE,
        IssuerSigningKey = AuthConfiguration.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("BeAdmin", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.Requirements.Add(new RoleRequirement());
    });
});
builder.Services.AddSingleton<IAuthorizationHandler, RoleRequimentHandler>();

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
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/exception", () =>
{
    throw new ArgumentNullException("There is nothing. Literally");
});

app.MapControllers();

app.Run();
