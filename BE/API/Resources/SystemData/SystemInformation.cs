namespace API.Resources.SystemData;

public sealed class SystemInformation
{
    public static string? Version { get; private set; }
    public static string? ApplicationName { get; private set; }
    public static string? Node { get; private set; }
}
