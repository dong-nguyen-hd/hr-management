using Business.Domain.Repositories;
using Business.Domain.Services;
using Business.Resources;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Business.Services.CronJob;

public class DeleteExpiredTokenJob : CronJobService
{
    #region Property
    private readonly IServiceProvider _serviceProvider;
    #endregion

    #region Constructor
    public DeleteExpiredTokenJob(IServiceProvider serviceProvider,
        IScheduleConfig<DeleteExpiredTokenJob> config) : base(config.CronExpression, config.TimeZoneInfo)
    {
            this._serviceProvider = serviceProvider;
        }
    #endregion

    #region Method
    public override async Task DoWorkAsync(CancellationToken cancellationToken)
    {
            try
            {
                Log.Information("DeleteRefreshTokenJob is working.");

                using var scope = _serviceProvider.CreateScope();

                var tokenRepository = scope.ServiceProvider.GetRequiredService<ITokenRepository>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                await DeleteExpiredTokenAsync(tokenRepository, unitOfWork);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "DeleteRefreshTokenJob fail.");
            }
        }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
            Log.Information("DeleteRefreshTokenJob is starting.");
            return base.StartAsync(cancellationToken);
        }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
            Log.Information("DeleteRefreshTokenJob is stopping.");
            return base.StopAsync(cancellationToken);
        }

    private async Task DeleteExpiredTokenAsync(ITokenRepository tokenRepository, IUnitOfWork unitOfWork)
    {
            var now = DateTime.UtcNow;

            var tempTokens = await tokenRepository.FindAsync(x => DateTime.Compare(x.ExpireTime, now) < 0 || x.IsUsed);

            tokenRepository.RemoveRange(tempTokens);

            await unitOfWork.CompleteAsync();
        }
    #endregion
}