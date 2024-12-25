using System.Security.Claims;
using ToDoAPI_ASPNET.Models.Config;
using ToDoAPI_ASPNET.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ToDoAPI_ASPNET.Models.Dtos.Auth;


namespace ToDoAPI_ASPNET.Services.Utils;

public class TokenUtil
{
    public enum TokenType
    {
        REFRESH,
        ACCESS
    }
    public static string GenerateToken(User user, JWTSettings jwt, TokenType tokenType)
    {
        var expires = DateTime.UtcNow;

        var claims = new List<Claim> {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

        switch (tokenType)
        {
            case TokenType.REFRESH:
                expires = expires.AddDays(jwt.RefreshTokenExpiry);
                break;

            case TokenType.ACCESS:
                expires = expires.AddDays(jwt.AccessTokenExpiry);
                claims.Add(new(ClaimTypes.Email, user.Email));
                claims.Add(new(ClaimTypes.NameIdentifier, user.Id.ToString()));
                break;
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
            Access = GenerateToken(user, jwt, TokenType.ACCESS),
            Refresh = GenerateToken(user, jwt, TokenType.REFRESH)
        };
    }

   

}
