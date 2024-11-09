namespace Business.Resources.DTOs.Information;

public class JwtConfig
{
    public static string Secret { get; set; }
    public static string Issuer { get; set; }
    public static string Audience { get; set; }
    public static int AccessTokenExpiration { get; set; }
    public static int RefreshTokenExpiration { get; set; }
}