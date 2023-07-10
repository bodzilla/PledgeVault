using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts.Entities.Validators;
using PledgeVault.Core.Enums;
using PledgeVault.Core.Exceptions;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Validators;

/// <summary>
/// <inheritdoc cref="IPartyEntityValidator"/>
/// </summary>
public sealed class PartyEntityValidator : IPartyEntityValidator
{
    private readonly PledgeVaultContext _context;

    public PartyEntityValidator(PledgeVaultContext context) => _context = context;

    public async Task ValidateAllRules(EntityValidatorType type, Party entity, CancellationToken cancellationToken)
    {
        if (type is not EntityValidatorType.Add) await EnsureEntityExists(entity, cancellationToken);
        await EnsureCountryExists(entity, cancellationToken);
        await EnsureNameWithCountryIdIsUnique(entity, cancellationToken);
    }

    public async Task EnsureEntityExists(Party entity, CancellationToken cancellationToken)
        => await EntityValidator.EnsurePartyExists(_context, entity.Id, cancellationToken);

    public async Task EnsureCountryExists(Party entity, CancellationToken cancellationToken)
        => await EntityValidator.EnsureCountryExists(_context, entity.CountryId, cancellationToken);

    public async Task EnsureNameWithCountryIdIsUnique(Party entity, CancellationToken cancellationToken)
    {
        if (await _context.Parties
                .AsNoTracking()
                .WithOnlyActiveEntities()
                .AnyAsync(x => EF.Functions.Like(x.Name, entity.Name) && x.CountryId == entity.CountryId, cancellationToken))
            throw new EntityValidationException($"{nameof(Party.Name)} with {nameof(Party.CountryId)} already exists.");
    }
}