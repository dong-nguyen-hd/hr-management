using Cronos;
using Microsoft.Extensions.Hosting;

namespace Business.Domain.Services;

public abstract class CronJobService : IHostedService, IDisposable
{
    #region Property
    private System.Timers.Timer _timer;
    private readonly CronExpression _expression;
    private readonly TimeZoneInfo _timeZoneInfo;
    #endregion

    #region Constructor
    protected CronJobService(string cronExpression, TimeZoneInfo timeZoneInfo)
    {
            this._expression = CronExpression.Parse(cronExpression);
            this._timeZoneInfo = timeZoneInfo;
        }
    #endregion

    #region Method
    public virtual async Task StartAsync(CancellationToken cancellationToken)
    {
            await ScheduleJobAsync(cancellationToken);
        }

    protected virtual async Task ScheduleJobAsync(CancellationToken cancellationToken)
    {
            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);

            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;
                if (delay.TotalMilliseconds <= 0) // Prevent non-positive values from being passed into Timer
                {
                    await ScheduleJobAsync(cancellationToken);
                }
                _timer = new System.Timers.Timer(delay.TotalMilliseconds);
                _timer.Elapsed += async (sender, args) =>
                {
                    _timer.Dispose(); // Reset and dispose timer
                    _timer = null;

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await DoWorkAsync(cancellationToken);
                    }

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await ScheduleJobAsync(cancellationToken); // Reschedule next
                    }
                };
                _timer.Start();
            }
            await Task.CompletedTask;
        }

    public virtual async Task DoWorkAsync(CancellationToken cancellationToken)
        => await Task.Delay(5000, cancellationToken); // Do the work in derive class

    public virtual async Task StopAsync(CancellationToken cancellationToken)
    {
            _timer?.Stop();
            await Task.CompletedTask;
        }

    public virtual void Dispose()
        => _timer?.Dispose();
    #endregion
}