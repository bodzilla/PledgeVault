using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Models;

namespace PledgeVault.Persistence.MappingConfigurations;

internal sealed class PartyConfiguration : IEntityTypeConfiguration<Party>
{
    public void Configure(EntityTypeBuilder<Party> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(250);
        builder.Property(x => x.LogoUrl).HasMaxLength(250);
        builder.Property(x => x.SiteUrl).HasMaxLength(250);
        builder.Property(x => x.Summary).HasMaxLength(10000);

        builder.Property(x => x.Name).IsRequired();

        builder.HasIndex(x => new { x.Name, x.CountryId }).IsUnique();

        builder.HasMany(x => x.Politicians)
            .WithOne(x => x.Party)
            .HasForeignKey(x => x.PartyId);
    }
}
