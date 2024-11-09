using Business.Domain.Services;
using Serilog;

namespace Business.Services;

public sealed class LogService : ILogService
{
    #region Method
    public void Write<T>(T? log) where T : class
    {
        var context = Log.ForContext("SourceContext", nameof(LogService));

        if (log == null)
            context.Error("Error log null");

        context.Information($"Logging {{@Model}}: {{@{nameof(T)}}}", nameof(T), log);
    }
    #endregion
}