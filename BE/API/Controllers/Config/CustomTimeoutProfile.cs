using Microsoft.AspNetCore.Http.Timeouts;

namespace API.Controllers.Config;

public static class CustomTimeoutProfile
{
    public const string Over15S = nameof(Over15S);
    public const string Over1M = nameof(Over1M);
    public const string Over3M = nameof(Over3M);

    public static void ApplyTimeoutProfile(this IServiceCollection services)
    {
        services.AddRequestTimeouts(options =>
        {
            options.DefaultPolicy = new RequestTimeoutPolicy
            {
                Timeout = TimeSpan.FromMilliseconds(15 * 1000)
            };

            options.AddPolicy(Over15S, TimeSpan.FromMilliseconds(15 * 1000));
            options.AddPolicy(Over1M, TimeSpan.FromMilliseconds(60 * 1000));
            options.AddPolicy(Over3M, TimeSpan.FromMilliseconds(3 * 60 * 1000));
        });
    }
}