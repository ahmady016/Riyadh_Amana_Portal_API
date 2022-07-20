using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Claims;
using System.Text;

using DB.Entities;

namespace Auth;

public static class AuthHelpers
{
    private static IConfiguration _config;
    public static void Initialize(IConfiguration Configuration)
    {
        _config = Configuration;
    }

    private static byte[] SecretKey => Encoding.UTF8.GetBytes(_config["JWT:Secret"]);
    private static int AccessTokenValidityHours => int.Parse(_config["JWT:AccessTokenValidityInHours"]);
    private static int RefreshTokenValidityDays => int.Parse(_config["JWT:RefreshTokenValidityInDays"]);
    private static string TokenIssuer => _config["JWT:Issuer"];
    private static string TokenAudience => _config["JWT:Audience"];

    public static string GenerateAccessToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) }),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SecretKey), SecurityAlgorithms.HmacSha256Signature),
            Issuer = TokenIssuer,
            Audience = TokenAudience,
            Expires = DateTime.Now.AddHours(AccessTokenValidityHours + 3),
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
            ExpiresAt = DateTime.UtcNow.AddHours(3).AddDays(RefreshTokenValidityDays),
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
            IssuerSigningKey = new SymmetricSecurityKey(SecretKey),
            ValidIssuer = TokenIssuer,
            ValidAudience = TokenAudience,
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
