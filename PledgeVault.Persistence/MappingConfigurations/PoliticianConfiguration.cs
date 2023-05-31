using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Models;

namespace PledgeVault.Persistence.MappingConfigurations;

internal sealed class PoliticianConfiguration : IEntityTypeConfiguration<Politician>
{
    public void Configure(EntityTypeBuilder<Politician> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.SexType).IsRequired();
        builder.Property(x => x.DateOfBirth).IsRequired();
        builder.Property(x => x.CountryOfBirth).IsRequired();
        builder.Property(x => x.GovernmentTitle).IsRequired();

        builder.HasMany(x => x.Pledges)
            .WithOne(x => x.Politician)
            .HasForeignKey(x => x.PoliticianId);
    }
}
