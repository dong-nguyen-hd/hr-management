using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Resources.DTOs.Information;

namespace API.Extensions;

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

    public static void AddJwtBearerAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true, // default True
                ValidIssuer = JwtConfig.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtConfig.Secret)),
                ValidAudience = JwtConfig.Audience,
                ValidateAudience = true, // default True
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(1)
            };
        });
    }
}