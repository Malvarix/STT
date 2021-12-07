using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using Quartz;
using STT.Application.Clients.Implementations.Imdb.Models.Common;
using STT.Application.Configurations;
using STT.Application.Dto.Request;
using STT.Application.Jobs.Extensions;
using STT.Application.Jobs.Options;
using STT.Application.Services.Interfaces;
using STT.Persistence;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace STT.Application.Jobs
{
    [DisallowConcurrentExecution]
    public class FilmRecommendationJob : IJob
    {
        private readonly ILogger<FilmRecommendationJob> _logger;
        private readonly IOptions<FilmRecommendationJobOptions> _jobOptions;
        private readonly IOptions<GmailSmtpConfigurationOptions> _gmailSmtpOptions;
        private readonly SttDbContext _context;
        private readonly IFilmService _filmService;

        public FilmRecommendationJob(
            ILogger<FilmRecommendationJob> logger,
            IOptions<FilmRecommendationJobOptions> jobOptions,
            IOptions<GmailSmtpConfigurationOptions> gmailSmtpOptions,
            SttDbContext context,
            IFilmService filmService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jobOptions = jobOptions ?? throw new ArgumentNullException(nameof(jobOptions));
            _gmailSmtpOptions = gmailSmtpOptions ?? throw new ArgumentNullException(nameof(gmailSmtpOptions));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _filmService = filmService ?? throw new ArgumentNullException(nameof(filmService));
        }

        // All of this logic inside MUST be decomposed
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation(Constants.TechnicalLevelMessages.JobHasBeenStarted);

                var watchlist = await _context.Watchlists.Include(w => w.WatchlistItems).FirstOrDefaultAsync(w => w.WatchlistId == _jobOptions.Value.WatchlistId);
                if (watchlist == null)
                {
                    _logger.LogInformation($"There is no one watchlist with Id {_jobOptions.Value.WatchlistId}.");
                    return;
                }

                if (watchlist?.WatchlistItems == null || !watchlist.WatchlistItems.Any())
                {
                    _logger.LogInformation($"Watchlist with Id {_jobOptions.Value.WatchlistId} has no one film.");
                    return;
                }

                if (watchlist?.WatchlistItems.Count(i => !i.IsWatched) < _jobOptions.Value.NotWatchedCountOfFilms)
                {
                    _logger.LogInformation($"Watchlist with Id {_jobOptions.Value.WatchlistId} has less not watched count of films than " +
                        $"{_jobOptions.Value.NotWatchedCountOfFilms}.");
                    return;
                }

                var notWatchedFilms = watchlist?.WatchlistItems
                    .Where(i => !i.IsWatched && i.MonthRecommendationsCount < _jobOptions.Value.MonthFilmRecommendationsCountLimit);

                if (notWatchedFilms == null || !notWatchedFilms.Any())
                {
                    _logger.LogInformation($"No one not watched film from watchlist with Id {_jobOptions.Value.WatchlistId} fits for executing job logic.");
                    return;
                }

                var notWatchedFilmsRatings = new List<RatingData>();

                foreach (var notWatchedFilm in notWatchedFilms)
                {
                    try
                    {
                        var filmIdRequestDto = new FilmIdRequestDto
                        {
                            Id = notWatchedFilm.FilmId
                        };

                        notWatchedFilmsRatings.Add(await _filmService.GetFilmRatingsAsync(filmIdRequestDto, context.CancellationToken));
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError(exception, $"Error has hapenned on fetching rating data of the film with Id {notWatchedFilm.FilmId}.");
                    }
                }

                var theMostRatedNotWatchedFilm = notWatchedFilmsRatings
                    .Where(f => !string.IsNullOrWhiteSpace(f.IMDb))
                    .OrderByDescending(f => double.Parse(f.IMDb, CultureInfo.InvariantCulture))
                    .FirstOrDefault();

                if (theMostRatedNotWatchedFilm == null)
                {
                    _logger.LogInformation("Selected films haven't got review mark from IDMb.");
                    return;
                }

                var theMostRatedFilmIdRequestDto = new FilmIdRequestDto { Id = theMostRatedNotWatchedFilm.IMDbId };

                var filmPostersData = await _filmService.GetFilmPostersAsync(theMostRatedFilmIdRequestDto, context.CancellationToken);
                var firstFilmPoster = filmPostersData.Posters.First();
                if (firstFilmPoster == null)
                {
                    _logger.LogInformation($"Film with Id {filmPostersData.IMDbId} has no one poster.");
                }

                var filmDescription = await _filmService.GetFilmDescriptionFromWikipedia(theMostRatedFilmIdRequestDto, context.CancellationToken);

                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Watchlist service", _jobOptions.Value?.NetworkCredentials?.UserName));
                emailMessage.To.Add(new MailboxAddress(nameof(FilmRecommendationJobOptions.UserEmail), _jobOptions.Value?.UserEmail));
                emailMessage.Subject = "HOT! Weekly film recommendation!";
                emailMessage.Body = new TextPart(TextFormat.Html)
                {
                    Text = $"<h2>Film - {filmPostersData.FullTitle}</h2><br>" +
                           $"IMDb rating is awesome - <b>{theMostRatedNotWatchedFilm.IMDb}!<b><br>" +
                           $"<i>Fascinating plot will defenitely gripping you!</i> {filmDescription.PlotShort?.Html}<br>" +
                           $"<b>{filmPostersData.Title} is waiting for you!<b><br><br>" +
                           (firstFilmPoster == null ? string.Empty : $"<img src='{firstFilmPoster.Link}' alt='{filmPostersData.Title}' width='{firstFilmPoster.Width}' height='{firstFilmPoster.Height}'>")
                };

                try
                {
                    using var smtpClient = new SmtpClient();

                    smtpClient.CheckCertificateRevocation = false;

                    await smtpClient.ConnectAsync(
                        _gmailSmtpOptions.Value.Host,
                        _gmailSmtpOptions.Value.PortNumber,
                        false,
                        context.CancellationToken);

                    await smtpClient.AuthenticateAsync(
                        _jobOptions.Value?.NetworkCredentials?.UserName,
                        _jobOptions.Value?.NetworkCredentials?.Password,
                        context.CancellationToken);

                    await smtpClient.SendAsync(emailMessage);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Sending recommendation email failed.");
                    return;
                }

                var successfullyRecommendedFilm = watchlist?.WatchlistItems.FirstOrDefault(f => f.FilmId == theMostRatedNotWatchedFilm.IMDbId);
                if (successfullyRecommendedFilm == null)
                {
                    throw new NullReferenceException(nameof(successfullyRecommendedFilm));
                }

                successfullyRecommendedFilm.MonthRecommendationsCount++;

                await Task.Run(async () =>
                {
                    _context.WatchlistItems?.Update(successfullyRecommendedFilm);
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