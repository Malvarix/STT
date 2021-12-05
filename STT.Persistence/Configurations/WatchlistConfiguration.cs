using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using STT.Domain.Entities;
using STT.Persistence.Generators;

namespace STT.Persistence.Configurations
{
    internal class WatchlistConfiguration : IEntityTypeConfiguration<Watchlist>
    {
        public void Configure(EntityTypeBuilder<Watchlist> builder)
        {
            builder.Property(p => p.WatchlistId)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .HasValueGenerator<GuidValueGenerator>()
                .IsRequired();

            builder.Property(p => p.Title)
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

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.HasMany(p => p.WatchlistItems);
        }
    }
}