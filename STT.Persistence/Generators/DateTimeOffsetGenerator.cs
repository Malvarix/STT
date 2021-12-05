using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace STT.Persistence.Generators
{
    public class DateTimeOffsetGenerator : ValueGenerator<DateTimeOffset>
    {
        public override bool GeneratesTemporaryValues { get; }

        public override DateTimeOffset Next(EntityEntry entry)
        {
            return DateTimeOffset.UtcNow;
        }
    }
}