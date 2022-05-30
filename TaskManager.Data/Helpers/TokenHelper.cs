using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;
using TaskManager.Core.Models.Data;

namespace TaskManager.Data.Helpers;

public static class TokenHelper
{
    public static string GetToken(ApplicationUser user, IConfiguration configuration)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        byte[] tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
        SymmetricSecurityKey key = new(tokenKey);
        JwtSecurityToken token = new(
            issuer: configuration["JWT:Issuer"],
            audience: configuration["JWT:Audience"],
            new Claim[]
            {
                    new Claim("Id", $"{user.Id}"),
                    new Claim("FirstName", $"{user.FirstName}"),
                    new Claim("LastName", $"{user.LastName}"),
                    new Claim("MiddleName", $"{user.MiddleName}"),
                    new Claim("PhoneNumber", $"{user.PhoneNumber}"),
                    new Claim("Email", user.Email),
                    new Claim(ClaimTypes.Role, user.RoleName)
            },
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new(key, SecurityAlgorithms.HmacSha256Signature));
        string encodedToken = tokenHandler.WriteToken(token);
        return encodedToken;
    }

    public static string? GetRole(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        return tokenHandler.ReadJwtToken(token).Claims.FirstOrDefault(c => c.Type == "Role")?.Value;
    }

    public static string? GetId(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        return tokenHandler.ReadJwtToken(token).Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
    }

    public static IEnumerable<Claim> DecodeToken(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        return tokenHandler.ReadJwtToken(token).Claims;
    }
}
