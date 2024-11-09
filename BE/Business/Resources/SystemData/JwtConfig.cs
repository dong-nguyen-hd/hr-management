namespace Business.Resources.SystemData;

public sealed class JwtConfig
{
    public static string? Secret { get; private set; }
    public static string? Issuer { get; private set; }
    public static string? Audience { get; private set; }
    public static int AccessTokenExpiration { get; private set; }
    public static int RefreshTokenExpiration { get; private set; }
}
