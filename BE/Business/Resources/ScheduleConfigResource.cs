namespace Business.Resources;

public interface IScheduleConfig<T>
{
    #region Property
    string CronExpression { get; set; }
    TimeZoneInfo TimeZoneInfo { get; set; }
    #endregion
}

public class ScheduleConfig<T> : IScheduleConfig<T>
{
    #region Property
    public string CronExpression { get; set; }
    public TimeZoneInfo TimeZoneInfo { get; set; }
    #endregion
}