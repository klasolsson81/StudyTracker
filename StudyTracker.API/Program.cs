using Scalar.AspNetCore;
using StudyTracker.API.ExceptionHandlers;
using StudyTracker.Application;
using StudyTracker.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Application-lagret (MediatR + handlers + validators) samt Infrastructure (EF Core + repositories).
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

// ValidationException (från MediatR ValidationBehavior) mappas till 400 Bad Request.
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    // OpenAPI-dokumentet samt Scalar-UI på /scalar/v1
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
