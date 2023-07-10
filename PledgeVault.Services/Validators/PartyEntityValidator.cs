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
    {
        if (await _context.Parties
                .AsNoTracking()
                .WithOnlyActiveEntities()
                .SingleOrDefaultAsync(x => x.Id == entity.Id, cancellationToken) is null)
            throw new InvalidEntityException($"{nameof(Party)} not found with {nameof(Party.Id)}: '{entity.Id}'.");
    }

    public async Task EnsureCountryExists(Party entity, CancellationToken cancellationToken)
    {
        if (await _context.Countries
                .AsNoTracking()
                .WithOnlyActiveEntities()
                .SingleOrDefaultAsync(x => x.Id == entity.CountryId, cancellationToken) is null)
            throw new InvalidEntityException($"{nameof(Country)} not found with {nameof(Country.Id)}: '{entity.CountryId}'.");
    }

    public async Task EnsureNameWithCountryIdIsUnique(Party entity, CancellationToken cancellationToken)
    {
        if (await _context.Parties
                .AsNoTracking()
                .WithOnlyActiveEntities()
                .SingleOrDefaultAsync(x => EF.Functions.Like(x.Name, entity.Name) && x.CountryId == entity.CountryId, cancellationToken) is not null)
            throw new InvalidEntityException($"{nameof(Party.Name)} with {nameof(Party.CountryId)} already exists.");
    }
}