namespace API.Controllers.Config;

public static class CustomCacheProfile
{
    #region Profiles
    public const string NoCache = nameof(NoCache);
    public const string Any60S = nameof(Any60S);
    public const string Any5M = nameof(Any5M);
    #endregion

    #region Method
    public static void ApplyCacheProfile(this MvcOptions opt)
    {
        opt.CacheProfiles.Add(NoCache, new CacheProfile() { NoStore = true });
        opt.CacheProfiles.Add(Any60S, new CacheProfile()
        {
            Location = ResponseCacheLocation.Any,
            Duration = 60
        });
        opt.CacheProfiles.Add(Any5M, new CacheProfile()
        {
            Location = ResponseCacheLocation.Any,
            Duration = 5*60
        });
    }
    #endregion
}