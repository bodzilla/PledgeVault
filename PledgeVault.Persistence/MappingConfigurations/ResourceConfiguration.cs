using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Models;

namespace PledgeVault.Persistence.MappingConfigurations;

internal sealed class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(250);
        builder.Property(x => x.SiteUrl).HasMaxLength(250);
        builder.Property(x => x.Summary).HasMaxLength(10000);

        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.SiteUrl).IsRequired();
        builder.Property(x => x.ResourceType).IsRequired();

        builder.HasIndex(x => new { x.SiteUrl, x.PledgeId }).IsUnique();
    }
}
