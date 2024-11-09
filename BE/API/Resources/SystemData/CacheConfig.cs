namespace API.Resources.SystemData;

public sealed class CacheConfig
{
    public static string? RedisUri { get; private set; }
    public static string? RedisInstanceName { get; private set; }
    public static bool UseRedis { get; private set; }
}
