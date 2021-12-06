using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STT.Domain.Entities;
using STT.Persistence.Extensions;

namespace STT.Persistence.Configurations
{
    internal class WatchlistConfiguration : IEntityTypeConfiguration<Watchlist>
    {
        public void Configure(EntityTypeBuilder<Watchlist> builder)
        {
            builder.Property(p => p.WatchlistId)
                .HasColumnName("Id")
                .HasDefaultValueSql(Constants.MsSqlFunctions.NewId)
                .IsRequired();

            builder.Property(p => p.Title)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql(Constants.MsSqlFunctions.SysDateTimeOffset)
                .IsRequired();

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.HasMany(p => p.WatchlistItems);
        }
    }
}