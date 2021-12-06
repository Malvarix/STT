using Microsoft.EntityFrameworkCore;
using STT.Domain.Entities;

namespace STT.Persistence
{
    public class SttDbContext : DbContext
    {
        public DbSet<Watchlist>? Watchlists { get; set; }
        public DbSet<WatchlistItem>? WatchlistItems { get; set; }

        public SttDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SttDbContext).Assembly);
        }
    }
}