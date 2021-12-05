using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STT.Domain.Entities;
using System;

namespace STT.Persistence.Configurations
{
    internal class WatchlistItemConfiguration : IEntityTypeConfiguration<WatchlistItem>
    {
        public void Configure(EntityTypeBuilder<WatchlistItem> builder)
        {
            builder.Property(p => p.WatchlistItemId)
                .HasColumnName("Id")
                .HasDefaultValue(Guid.NewGuid())
                .IsRequired();

            builder.Property(p => p.WatchlistId)
                .IsRequired();

            builder.Property(p => p.FilmId)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(p => p.CreatedOn)
                .HasDefaultValue(DateTime.UtcNow)
                .IsRequired();

            builder.Property(p => p.IsWatched)
                .IsRequired();

            builder.HasOne(p => p.Watchlist);
        }
    }
}