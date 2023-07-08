using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts.Entities.Validators;
using PledgeVault.Core.Enums;
using PledgeVault.Core.Exceptions;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
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

    public async Task ValidateAllRules(EntityValidatorType type, Country entity, CancellationToken cancellationToken)
    {
        if (type is not EntityValidatorType.Add) await EnsureEntityExists(entity, cancellationToken);
        await EnsureNameIsUnique(entity, cancellationToken);
    }

    public async Task EnsureEntityExists(Country entity, CancellationToken cancellationToken)
        => await EntityValidator.EnsureCountryExists(_context, entity.Id, cancellationToken);

    public async Task EnsureNameIsUnique(Country entity, CancellationToken cancellationToken)
    {
        if (await _context.Countries
                .AsNoTracking()
                .AnyAsync(x => EF.Functions.Like(x.Name, entity.Name), cancellationToken))
            throw new EntityValidationException($"{nameof(Country.Name)} already exists: '{entity.Name}'.");
    }
}