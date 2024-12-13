using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Helpers;

public static class AuthHelpers
{
    public static string GenerateToken()
    {
        var securityKey = new SymmetricSecurityKey("ItsAVeryLongSecretToNotCrashTheCodeForNow"u8.ToArray());
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}