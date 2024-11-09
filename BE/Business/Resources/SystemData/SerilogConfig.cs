namespace Business.Resources.SystemData;

public sealed class SerilogConfig
{
    public static bool EnableConsoleLog { get; private set; }
    
    public static bool EnableFileLog { get; private set; }
    public static string? PathFileLog { get; private set; }
    
    public static bool EnableElasticsearchLog { get; private set; }
    public static string? ElasticsearchUri { get; private set; }
    public static string? ElasticsearchIndexFormat { get; private set; }
    public static string? ElasticsearchUsername { get; private set; }
    public static string? ElasticsearchPassword { get; private set; }
}
