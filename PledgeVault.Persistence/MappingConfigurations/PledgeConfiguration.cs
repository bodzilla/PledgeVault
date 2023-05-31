using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Models;

namespace PledgeVault.Persistence.MappingConfigurations;

internal sealed class PledgeConfiguration : IEntityTypeConfiguration<Pledge>
{
    public void Configure(EntityTypeBuilder<Pledge> builder)
    {
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.DatePledged).IsRequired();
        builder.Property(x => x.PledgeCategoryType).IsRequired();
        builder.Property(x => x.PledgeStatusType).IsRequired();

        builder.HasIndex(x => new { x.Title, x.DatePledged, x.PoliticianId }).IsUnique();

        builder.HasMany(x => x.Resources)
            .WithOne(x => x.Pledge)
            .HasForeignKey(x => x.PledgeId);
    }
}
