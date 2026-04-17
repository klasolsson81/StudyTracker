using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudyTracker.Application.DTOs.Auth;
using StudyTracker.Application.Interfaces;
using StudyTracker.Infrastructure.Identity;

namespace StudyTracker.Infrastructure.Services;

// Sköter registrering, inloggning och JWT-generering.
// Läser JWT-konfig (Secret, Issuer, Audience, ExpirationMinutes) från IConfiguration.
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<AuthResponseDto?> RegisterAsync(string userName, string email, string password, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded) return null;

        return await BuildAuthResponseAsync(user);
    }

    public async Task<AuthResponseDto?> LoginAsync(string userName, string password, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null) return null;

        var passwordOk = await _userManager.CheckPasswordAsync(user, password);
        if (!passwordOk) return null;

        return await BuildAuthResponseAsync(user);
    }

    private async Task<AuthResponseDto> BuildAuthResponseAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var secret = _configuration["Jwt:Secret"]
            ?? throw new InvalidOperationException("Jwt:Secret saknas i konfigurationen.");
        var issuer = _configuration["Jwt:Issuer"] ?? "StudyTracker";
        var audience = _configuration["Jwt:Audience"] ?? "StudyTrackerUsers";
        var expirationMinutes = int.TryParse(_configuration["Jwt:ExpirationMinutes"], out var m) ? m : 60;

        var expiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes);

        // Claims: sub = userId, name = användarnamn, email + en roll-claim per roll.
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Name, user.UserName ?? string.Empty),
            new(ClaimTypes.Email, user.Email ?? string.Empty)
        };
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new AuthResponseDto(tokenString, expiresAt, user.UserName ?? string.Empty, roles);
    }
}
