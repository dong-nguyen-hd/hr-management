namespace Business.Extensions.AddConfig;

public static class RelateLogConfig
{
    public static ILogger LogWithContext(this string context) =>
        Log.ForContext("SourceContext", context);
}