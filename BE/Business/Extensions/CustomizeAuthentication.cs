namespace Business.Extensions;

public static class CustomizeAuthentication
{
    public static (string? id, string? token) ComputeRefreshTokenId(this string? refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
            return (null, null);

        var tokenSplit = refreshToken.Split('_', StringSplitOptions.RemoveEmptyEntries);
        if (tokenSplit.Length != 2)
            return (null, null);

        return (tokenSplit[0], tokenSplit[1]);
    }
}