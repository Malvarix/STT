using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STT.Domain.Entities;
using STT.Persistence.Extensions;

namespace STT.Persistence.Configurations
{
    internal class WatchlistItemConfiguration : IEntityTypeConfiguration<WatchlistItem>
    {
        public void Configure(EntityTypeBuilder<WatchlistItem> builder)
        {
            builder.Property(p => p.WatchlistItemId)
                .HasColumnName("Id")
                .HasDefaultValueSql(Constants.MsSqlFunctions.NewId)
                .IsRequired();

            builder.Property(p => p.WatchlistId)
                .IsRequired();

            builder.Property(p => p.FilmId)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql(Constants.MsSqlFunctions.SysDateTimeOffset)
                .IsRequired();

            builder.Property(p => p.IsWatched)
                .IsRequired();

            builder.Property(p => p.MonthRecommendationsCount)
                .HasDefaultValueSql("0")
                .IsRequired();

            builder.HasOne(p => p.Watchlist);
        }
    }
}