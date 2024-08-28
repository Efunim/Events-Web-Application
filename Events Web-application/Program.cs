using Events.Application.DTO.MappingProfiles;
using Events.Application.Interfaces.UseCases;
using Events.Application.UseCases;
using Events.Domain.Repositories;
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

#region Mappers
builder.Services.AddAutoMapper(typeof(EventCategoryMappingProfile));
builder.Services.AddAutoMapper(typeof(EventMappingProfile));
builder.Services.AddAutoMapper(typeof(LocationMappingProfile));
builder.Services.AddAutoMapper(typeof(ParticipantMappingProfile));
builder.Services.AddAutoMapper(typeof(UserMappingProfile));
#endregion

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

#region Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEventCategoryRepository, EventCategoryRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

builder.Services.AddScoped<IAuthentificationService, AuthentificationService>();

#region UseCases
#region EventCategory
builder.Services.AddScoped<IAddEventCategoryUseCase, AddEventCategoryUseCase>();
builder.Services.AddScoped<IGetEventCategoryUseCase, GetEventCategoryUseCase>();
builder.Services.AddScoped<IGetPageEventCategoriesUseCase, GetPageEventCategoriesUseCase>();
builder.Services.AddScoped<IUpdateEventCategoryUseCase, UpdateEventCategoryUseCase>();
builder.Services.AddScoped<IDeleteEventCategoryUseCase, DeleteEventCategoryUseCase>();
#endregion
#region Event
builder.Services.AddScoped<IAddEventUseCase, AddEventUseCase>();
builder.Services.AddScoped<IGetEventUseCase, GetEventUseCase>();
builder.Services.AddScoped<IGetEventsPageUseCase, GetEventsPageUseCase>();
builder.Services.AddScoped<IUpdateEventUseCase, UpdateEventUseCase>();
builder.Services.AddScoped<IDeleteEventUseCase, DeleteEventUseCase>();
#endregion
#region Location
builder.Services.AddScoped<IAddLocationUseCase, AddLocationUseCase>();
builder.Services.AddScoped<IGetLocationUseCase, GetLocationUseCase>();
builder.Services.AddScoped<IGetLocationsPageUseCase, GetLocationsPageUseCase>();
builder.Services.AddScoped<IUpdateLocationUseCase, UpdateLocationUseCase>();
builder.Services.AddScoped<IDeleteLocationUseCase, DeleteLocationUseCase>();
#endregion
#region Participant
builder.Services.AddScoped<IAddParticipantUseCase, AddParticipantUseCase>();
builder.Services.AddScoped<IGetParticipantUseCase, GetParticipantUseCase>();
builder.Services.AddScoped<IGetParticipantsPageUseCase, GetParticipantsPageUseCase>();
builder.Services.AddScoped<IUpdateParticipantUseCase, UpdateParticipantUseCase>();
builder.Services.AddScoped<IDeleteParticipantUseCase, DeleteParticipantUseCase>();
#endregion
#region User
builder.Services.AddScoped<IAddUserUseCase, AddUserUseCase>();
builder.Services.AddScoped<IGetUserUseCase, GetUserUseCase>();
builder.Services.AddScoped<IGetUsersPageUseCase, GetUsersPageUseCase>();
builder.Services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
builder.Services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
builder.Services.AddScoped<IStoreRefreshTokenUseCase, StoreRefreshTokenUseCase>();
#endregion
#endregion

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
