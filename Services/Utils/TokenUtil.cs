using System.Security.Claims;
using ToDoAPI_ASPNET.Models.Config;
using ToDoAPI_ASPNET.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ToDoAPI_ASPNET.Models.Dtos.Auth;


namespace ToDoAPI_ASPNET.Services.Utils;

public class TokenUtil
{
    public static string GenerateToken(User user, JWTSettings jwt, Token.TokenType type)
    {
        var now = DateTime.Now;
        var expires = type switch
        {
            Token.TokenType.Refresh => now.AddDays(jwt.RefreshTokenExpiry),
            Token.TokenType.Access => now.AddMinutes(jwt.AccessTokenExpiry),
            _ => now.AddMinutes(15)
        };

        var claims = new List<Claim> {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

        if (type == Token.TokenType.Access)
        {
            claims.Add(new(ClaimTypes.Email, user.Email));
            claims.Add(new(ClaimTypes.NameIdentifier, user.Id.ToString()));
        }

        claims.Add(new(JwtRegisteredClaimNames.Exp,
        new DateTimeOffset(expires).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));

        var key = new SymmetricSecurityKey(Base64UrlEncoder.DecodeBytes(jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwt.Issuer,
            audience: jwt.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static AuthResponseDto GenerateTokens(User user, JWTSettings jwt)
    {
        return new AuthResponseDto
        {
            Access = GenerateToken(user, jwt, Token.TokenType.Access),
            Refresh = GenerateToken(user, jwt, Token.TokenType.Refresh)
        };
    }

    public static ClaimsPrincipal? ValidateToken(string token, JWTSettings jwt, IHostEnvironment environment)
    {
        var isDevelopment = environment.IsDevelopment();
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Base64UrlEncoder.DecodeBytes(jwt.Key);

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = !isDevelopment,
                ValidIssuer = jwt.Issuer,
                ValidateAudience = !isDevelopment,
                ValidAudience = jwt.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
            }, out SecurityToken validatedToken);

            return principal;
        }
        catch (Exception)
        {
            return null;
        }
    }

}
