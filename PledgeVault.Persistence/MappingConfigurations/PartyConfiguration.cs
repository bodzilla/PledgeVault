using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Models;

namespace PledgeVault.Persistence.MappingConfigurations;

internal sealed class PartyConfiguration : IEntityTypeConfiguration<Party>
{
    public void Configure(EntityTypeBuilder<Party> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasMany(x => x.Politicians)
            .WithOne(x => x.Party)
            .HasForeignKey(x => x.PartyId);
    }
}
