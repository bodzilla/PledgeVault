using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Models;

namespace PledgeVault.Persistence.MappingConfigurations;

internal sealed class PoliticianConfiguration : IEntityTypeConfiguration<Politician>
{
    public void Configure(EntityTypeBuilder<Politician> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(250);
        builder.Property(x => x.CountryOfBirth).HasMaxLength(250);
        builder.Property(x => x.PhotoUrl).HasMaxLength(250);
        builder.Property(x => x.Summary).HasMaxLength(10000);

        builder.Property(x => x.Name).IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.SexType).IsRequired();
        builder.Property(x => x.DateOfBirth).IsRequired();
        builder.Property(x => x.CountryOfBirth).IsRequired();

        builder.HasMany(x => x.Pledges)
            .WithOne(x => x.Politician)
            .HasForeignKey(x => x.PoliticianId);
    }
}
