using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using StudyTracker.API.ExceptionHandlers;
using StudyTracker.API.OpenApi;
using StudyTracker.Application;
using StudyTracker.Infrastructure;
using StudyTracker.Infrastructure.Database;
using StudyTracker.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// OpenAPI + Bearer-scheme-transformer → Scalar-UI får en Authorize-knapp.
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

// Application-lagret (MediatR + handlers + validators) samt Infrastructure (EF Core + Identity + repositories).
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

// JWT-autentisering. Secret/Issuer/Audience läses från IConfiguration.
// I Development läses Secret från user-secrets; i produktion från miljövariabel eller secret-store.
// appsettings.json har en placeholder som guarden nedan blockerar — fail fast hellre än att starta med svag nyckel.
const string placeholderSecret = "REPLACE_THIS_WITH_A_LONG_RANDOM_SECRET_OF_AT_LEAST_32_CHARS";

var jwtSecret = builder.Configuration["Jwt:Secret"]
    ?? throw new InvalidOperationException("Jwt:Secret saknas i konfigurationen.");

if (jwtSecret == placeholderSecret)
{
    throw new InvalidOperationException(
        "Jwt:Secret är kvar på placeholder-värdet. Sätt en riktig secret via `dotnet user-secrets set Jwt:Secret <värde>` eller en miljövariabel.");
}

if (Encoding.UTF8.GetByteCount(jwtSecret) < 32)
{
    throw new InvalidOperationException("Jwt:Secret måste vara minst 32 bytes för HMAC-SHA256.");
}

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

// Health-check på /health med DbContext-readiness-check mot AppDbContext.
builder.Services
    .AddHealthChecks()
    .AddDbContextCheck<AppDbContext>("database");

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

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
