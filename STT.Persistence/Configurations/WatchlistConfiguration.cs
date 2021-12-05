using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STT.Domain.Entities;
using System;

namespace STT.Persistence.Configurations
{
    internal class WatchlistConfiguration : IEntityTypeConfiguration<Watchlist>
    {
        public void Configure(EntityTypeBuilder<Watchlist> builder)
        {
            builder.Property(p => p.WatchlistId)
                .HasColumnName("Id")
                .HasDefaultValue(Guid.NewGuid())
                .IsRequired();

            builder.Property(p => p.Title)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(p => p.CreatedOn)
                .HasDefaultValue(DateTime.UtcNow)
                .IsRequired();

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.HasMany(p => p.WatchlistItems);
        }
    }
}