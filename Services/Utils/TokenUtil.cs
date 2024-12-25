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

}
