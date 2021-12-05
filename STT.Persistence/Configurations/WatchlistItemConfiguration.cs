using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using STT.Domain.Entities;
using STT.Persistence.Generators;

namespace STT.Persistence.Configurations
{
    internal class WatchlistItemConfiguration : IEntityTypeConfiguration<WatchlistItem>
    {
        public void Configure(EntityTypeBuilder<WatchlistItem> builder)
        {
            builder.Property(p => p.WatchlistItemId)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .HasValueGenerator<GuidValueGenerator>()
                .IsRequired();

            builder.Property(p => p.WatchlistId)
                .IsRequired();

            builder.Property(p => p.FilmId)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .ValueGeneratedOnAdd()
                .HasValueGenerator<DateTimeOffsetGenerator>()
                .IsRequired();

            builder.Property(p => p.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasValueGenerator<DateTimeOffsetGenerator>()
                .IsRequired();

            builder.Property(p => p.IsWatched)
                .IsRequired();

            builder.HasOne(p => p.Watchlist);
        }
    }
}