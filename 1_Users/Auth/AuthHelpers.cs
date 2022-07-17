using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Claims;
using DB.Entities;

namespace Auth;

public static class AuthHelpers
{
    private static IConfiguration _config;
    public static void Initialize(IConfiguration Configuration)
    {
        _config = Configuration;
    }

    public static string GenerateAccessToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Convert.FromBase64String(_config["JWT:Secret"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) }),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _config["JWT:Issuer"],
            Audience = _config["JWT:Audience"],
            Expires = DateTime.Now.AddHours(int.Parse(_config["JWT:AccessTokenValidityInHours"])),
        };
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }

    private static string GetUniqueToken(List<string> usedRefreshTokens)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var tokenIsUnique = !usedRefreshTokens.Any(usedToken => usedToken == token);

        if (!tokenIsUnique)
            return GetUniqueToken(usedRefreshTokens);

        return token;
    }
    public static RefreshToken GenerateRefreshToken(Guid userId, List<string> usedRefreshTokens, string ipAddress = null)
    {
        return new RefreshToken
        {
            Value = GetUniqueToken(usedRefreshTokens),
            CreatedAt = DateTime.UtcNow.AddHours(3),
            ExpiresAt = DateTime.UtcNow.AddHours(3).AddDays(int.Parse(_config["JWT:RefreshTokenValidityInDays"])),
            CreatedIP = ipAddress,
            UserId = userId
        };
    }

    public static TokenValidationParameters GetTokenValidationOptions(bool validateLifetime)
    {
        return new TokenValidationParameters
        {
            ValidateLifetime = validateLifetime,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(_config["JWT:Secret"])),
            ValidIssuer = _config["JWT:Issuer"],
            ValidAudience = _config["JWT:Audience"],
        };
    }

    private static IEnumerable<Claim> ValidateTokenAndGetClaims(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenValidationParameters = GetTokenValidationOptions(validateLifetime: true);
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");
            
        return principal.Claims;
    }


}
