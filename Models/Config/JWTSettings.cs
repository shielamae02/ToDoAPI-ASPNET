namespace ToDoAPI_ASPNET.Models.Config;

public class JWTSettings
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int AccessTokenExpiry { get; set; }
    public int RefreshTokenExpiry { get; set; }
    public int ResetTokenExpiry { get; set; }
}
