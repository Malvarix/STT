using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace STT.Persistence.Extensions
{
    public static class SttDbContextService
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<SttDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        public static void DatabaseMigrate(this IServiceProvider applicationServices)
        {
            using var scope = applicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetService<SttDbContext>();
            context?.Database.Migrate();
        }
    }
}