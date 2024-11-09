using API.Domain.Services;
using API.Resources.SystemData;
using API.Services.CronJob;
using TimeZoneConverter;

namespace API.Extensions.AddConfig;

public static class RelateCronJob
{
    public static void RegisterCronJob(this IServiceCollection services)
    {
        // Every minute: * * * * *

        services.AddCronJob<DeleteExpiredTokenJob>(c =>
        {
            c.TimeZoneInfo = TZConvert.GetTimeZoneInfo(SystemConstant.LocalTimeZoneId);
            c.CronExpression = @"0 3 * * *"; // Every 3h AM
        });
    }

    #region Private work

    private static void AddCronJob<T>(this IServiceCollection services, Action<IScheduleConfig<T>> options) where T : CronJobService
    {
        if (options == null)
            throw new ArgumentNullException(nameof(options), "Please provide Schedule Configurations.");

        var config = new ScheduleConfig<T>();
        options.Invoke(config);

        if (string.IsNullOrWhiteSpace(config.CronExpression))
            throw new ArgumentNullException(nameof(ScheduleConfig<T>.CronExpression), "Empty Cron Expression is not allowed.");

        services.AddSingleton<IScheduleConfig<T>>(config);
        services.AddSingleton<T>();
        services.AddHostedService<T>();
    }

    #endregion
}

public interface IScheduleConfig<T>
{
    string? CronExpression { get; set; }
    TimeZoneInfo? TimeZoneInfo { get; set; }
}

public sealed class ScheduleConfig<T> : IScheduleConfig<T>
{
    public string? CronExpression { get; set; }
    public TimeZoneInfo? TimeZoneInfo { get; set; }
}