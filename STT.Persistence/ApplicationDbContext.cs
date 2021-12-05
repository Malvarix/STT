using Microsoft.EntityFrameworkCore;
using STT.Domain.Entities;

namespace STT.Persistence
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Watchlist>? Watchlists { get; set; }
        public DbSet<WatchlistItem>? WatchlistItems { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}