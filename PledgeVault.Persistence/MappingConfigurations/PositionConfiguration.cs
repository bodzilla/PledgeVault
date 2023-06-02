﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PledgeVault.Core.Models;

namespace PledgeVault.Persistence.MappingConfigurations;

internal sealed class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(250);
        builder.Property(x => x.Summary).HasMaxLength(10000);

        builder.Property(x => x.Title).IsRequired();
        builder.HasIndex(x => x.Title).IsUnique();

        builder.HasMany(x => x.Politicians)
            .WithOne(x => x.Position)
            .HasForeignKey(x => x.PositionId);
    }
}
