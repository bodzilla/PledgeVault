using PledgeVault.Persistence;
using System.Linq;
using HotChocolate.Data;
using HotChocolate.Types;
using PledgeVault.Core.Models;

namespace PledgeVault.Services.GraphQL;

public sealed class Queries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Country> GetCountries(PledgeVaultContext context) => context.Countries;

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Party> GetParties(PledgeVaultContext context) => context.Parties;

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Pledge> GetPledges(PledgeVaultContext context) => context.Pledges;

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Politician> GetPoliticians(PledgeVaultContext context) => context.Politicians;

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Resource> GetResources(PledgeVaultContext context) => context.Resources;
}