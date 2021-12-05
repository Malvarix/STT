using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace STT.Persistence.Extensions
{
    public static class ApplicationDbContextService
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        public static void DatabaseMigrate(this IServiceProvider applicationServices)
        {
            using var scope = applicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();
        }
    }
}