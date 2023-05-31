using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Models;
using PledgeVault.Persistence.MappingConfigurations;

namespace PledgeVault.Persistence;

public sealed class PledgeVaultContext : DbContext
{
    public DbSet<Country> Countries { get; set; }

    public DbSet<Party> Parties { get; set; }

    public DbSet<Politician> Politicians { get; set; }

    public DbSet<Position> Positions { get; set; }

    public DbSet<Pledge> Pledges { get; set; }

    public DbSet<Resource> Resources { get; set; }

    public PledgeVaultContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder) => builder
        .ApplyConfiguration(new CountryConfiguration())
        .ApplyConfiguration(new PartyConfiguration())
        .ApplyConfiguration(new PoliticianConfiguration())
        .ApplyConfiguration(new PositionConfiguration())
        .ApplyConfiguration(new PledgeConfiguration())
        .ApplyConfiguration(new ResourceConfiguration());
}
