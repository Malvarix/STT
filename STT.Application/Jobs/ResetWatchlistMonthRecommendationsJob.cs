using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;
using STT.Application.Jobs.Extensions;
using STT.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace STT.Application.Jobs
{
    [DisallowConcurrentExecution]
    public class ResetWatchlistMonthRecommendationsJob : IJob
    {
        private readonly ILogger<FilmRecommendationJob> _logger;
        private readonly SttDbContext _context;

        public ResetWatchlistMonthRecommendationsJob(
            ILogger<FilmRecommendationJob> logger, 
            SttDbContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation(Constants.TechnicalLevelMessages.JobHasBeenStarted);

                var notWatchedWatchlistItems = await _context.WatchlistItems.Where(i => !i.IsWatched).ToListAsync(context.CancellationToken);
                if (notWatchedWatchlistItems == null || !notWatchedWatchlistItems.Any())
                {
                    _logger.LogInformation("Exists no one watchlist item.");
                    return;
                }

                foreach (var watchlistItem in notWatchedWatchlistItems)
                {
                    watchlistItem.MonthRecommendationsCount = 0;
                }

                await Task.Run(async () =>
                {
                    _context.WatchlistItems?.UpdateRange(notWatchedWatchlistItems);
                    await _context.SaveChangesAsync(context.CancellationToken);
                });

                _logger.LogInformation(Constants.TechnicalLevelMessages.JobLogicSuccessfullyExecuted);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, Constants.TechnicalLevelMessages.ExceptionOccurredViaJobProcessing);
            }
            finally
            {
                _logger.LogInformation(Constants.TechnicalLevelMessages.JobHasBeenStopped);
            }
        }
    }
}