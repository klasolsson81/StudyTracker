using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using StudyTracker.API.ExceptionHandlers;
using StudyTracker.Application;
using StudyTracker.Infrastructure;
using StudyTracker.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Application-lagret (MediatR + handlers + validators) samt Infrastructure (EF Core + Identity + repositories).
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

// JWT-autentisering. Secret/Issuer/Audience läses från IConfiguration.
var jwtSecret = builder.Configuration["Jwt:Secret"]
    ?? throw new InvalidOperationException("Jwt:Secret saknas i konfigurationen.");
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "StudyTracker";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "StudyTrackerUsers";

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// ValidationException (från MediatR ValidationBehavior) mappas till 400 Bad Request.
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Kör identity-seed vid uppstart — skapar rollerna Admin/User + en default admin-användare.
using (var scope = app.Services.CreateScope())
{
    await IdentitySeeder.SeedAsync(scope.ServiceProvider);
}

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    // OpenAPI-dokumentet samt Scalar-UI på /scalar/v1
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

// Ordningen är viktig: Authentication måste köras FÖRE Authorization.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
