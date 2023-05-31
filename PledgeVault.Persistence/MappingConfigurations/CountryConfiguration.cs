using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Models;

namespace PledgeVault.Persistence.MappingConfigurations;

internal sealed class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.GovernmentType).IsRequired();

        builder.HasMany(x => x.Parties)
            .WithOne(x => x.Country)
            .HasForeignKey(x => x.CountryId);
    }
}
