using API.Extensions;
using API.Resources.Enums;

namespace API.Resources.SystemData;

public sealed class LogData
{
    public string Id { get; set; } = RelateText.GenId();

    public string? Node { get; set; }

    public string? ClientIp { get; set; }

    public string? TraceId
    {
        get => _traceId;
        set => _traceId = value?.Replace(':', '_');
    }

    private string? _traceId;

    public LogType? LogType { get; set; }

    public Dictionary<string, string>? RequestHeaders { get; set; } = new();

    public Dictionary<string, string>? RequestQueries { get; set; }

    public DateTime? RequestDatetimeUtc { get; set; }

    public string? RequestPath { get; set; }

    public string? RequestQuery { get; set; }

    public string? RequestMethod { get; set; }

    public string? RequestScheme { get; set; }

    public string? RequestHost { get; set; }

    public string? RequestBody { get; set; }

    public string? RequestContentType { get; set; }

    public DateTime? ResponseDatetimeUtc { get; set; }

    public string? ResponseStatus { get; set; }

    public string? ResponseBody { get; set; }

    public string? ResponseContentType { get; set; }

    public Dictionary<string, string>? ResponseHeaders { get; set; }

    public long TotalTimeMs => ComputeTotalTimeMs();

    public bool HasException { get; set; }

    public string? ExceptionMessage { get; set; }

    public string? ExceptionStackTrace { get; set; }

    #region Method

    private long ComputeTotalTimeMs()
    {
        if (ResponseDatetimeUtc == null || RequestDatetimeUtc == null)
            return 0;

        return Convert.ToInt64((ResponseDatetimeUtc.Value - RequestDatetimeUtc.Value).TotalMilliseconds);
    }

    #endregion
}