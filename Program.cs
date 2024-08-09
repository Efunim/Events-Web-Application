using Events_Web_application.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

/* SERVICES TO ADD 
 * Validation service
 * Authorization
 * Swagger
 * Entity services + UoW
 * AddControllers
*/

var app = builder.Build();

app.UseExceptionHandler();

app.MapGet("/exception", () =>
{
    throw new ArgumentNullException("There is nothing. Literally");
});
app.MapGet("/", () => "Hello World!");

app.Run();
