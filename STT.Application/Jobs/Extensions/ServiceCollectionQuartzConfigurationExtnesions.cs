using Microsoft.Extensions.Configuration;
using Quartz;
using System;

namespace STT.Application.Jobs.Extensions
{
    public static class ServiceCollectionQuartzConfigurationExtnesions
    {
        public static void AddJobAndTrigger<T> (
            this IServiceCollectionQuartzConfigurator quartz,
            IConfiguration config)
            where T : IJob
        {
            var jobName = typeof(T).Name;

            var configKey = $"Jobs:{jobName}:CronSchedule";
            var cronSchedule = config[configKey];

            if (string.IsNullOrWhiteSpace(cronSchedule))
            {
                throw new Exception($"Cron schedule hasn't been found for job '{jobName}' by the key '{configKey}'.");
            }

            var jobKey = new JobKey(jobName);
            quartz.AddJob<T>(options => options.WithIdentity(jobKey));

            quartz.AddTrigger(options => options
                .ForJob(jobKey)
                .WithIdentity(jobName + "-trigger")
                .WithCronSchedule(cronSchedule));
        }
    }
}