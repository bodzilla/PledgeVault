using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Models;
using PledgeVault.Persistence.MappingConfigurations;

namespace PledgeVault.Persistence;

public sealed class PledgeVaultContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Country> Countries { get; set; }

    public DbSet<Party> Parties { get; set; }

    public DbSet<Politician> Politicians { get; set; }

    public DbSet<Pledge> Pledges { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Resource> Resources { get; set; }

    public PledgeVaultContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder) => builder
        .ApplyConfiguration(new UserConfiguration())
        .ApplyConfiguration(new CountryConfiguration())
        .ApplyConfiguration(new PartyConfiguration())
        .ApplyConfiguration(new PoliticianConfiguration())
        .ApplyConfiguration(new PledgeConfiguration())
        .ApplyConfiguration(new CommentConfiguration())
        .ApplyConfiguration(new ResourceConfiguration());
}