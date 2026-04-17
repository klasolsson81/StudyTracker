using Scalar.AspNetCore;
using StudyTracker.Application;
using StudyTracker.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Application-lagret (MediatR + handlers) samt Infrastructure (EF Core + repositories).
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

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
