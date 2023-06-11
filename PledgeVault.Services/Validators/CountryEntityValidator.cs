using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts.Entities.Validators;
using PledgeVault.Core.Exceptions;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Validators;

/// <summary>
/// <inheritdoc cref="ICountryEntityValidator"/>
/// </summary>
public sealed class CountryEntityValidator : ICountryEntityValidator
{
    private readonly PledgeVaultContext _context;

    public CountryEntityValidator(PledgeVaultContext context) => _context = context;

    public async Task ValidateAllRules(Country entity, CancellationToken cancellationToken)
    {
        await EnsureEntityExists(entity, cancellationToken);
        await EnsureNameIsUnique(entity, cancellationToken);
    }

    public async Task EnsureEntityExists(Country entity, CancellationToken cancellationToken)
    {
        if (await _context.Countries
                .AsNoTracking()
                .WithOnlyActiveEntities()
                .SingleOrDefaultAsync(x => x.Id == entity.Id, cancellationToken) is null)
            throw new InvalidEntityException($"{nameof(Country)} not found with {nameof(Country.Id)}: '{entity.Id}'.");
    }

    public async Task EnsureNameIsUnique(Country entity, CancellationToken cancellationToken)
    {
        if (await _context.Countries
                .AsNoTracking()
                .SingleOrDefaultAsync(x => EF.Functions.Like(x.Name, entity.Name), cancellationToken) is not null)
            throw new InvalidEntityException($"{nameof(Country.Name)} already exists: '{entity.Name}'.");
    }
}