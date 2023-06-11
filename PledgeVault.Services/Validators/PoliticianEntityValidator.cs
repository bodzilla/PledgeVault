using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts.Entities.Validators;
using PledgeVault.Core.Exceptions;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Validators;

/// <summary>
/// <inheritdoc cref="IPoliticianEntityValidator"/>
/// </summary>
public sealed class PoliticianEntityValidator : IPoliticianEntityValidator
{
    private readonly PledgeVaultContext _context;

    public PoliticianEntityValidator(PledgeVaultContext context) => _context = context;

    public async Task ValidateAllRules(Politician entity, CancellationToken cancellationToken)
    {
        await EnsurePoliticianExists(entity, cancellationToken);
        await EnsurePartyExists(entity, cancellationToken);
        await EnsureOnlyOnePartyLeader(entity, cancellationToken);
    }

    public async Task EnsurePoliticianExists(Politician entity, CancellationToken cancellationToken)
    {
        if (await _context.Politicians.AsNoTracking().SingleOrDefaultAsync(x => x.Id == entity.Id, cancellationToken) is null)
            throw new InvalidRequestException($"{nameof(Politician)} not found with {nameof(Politician.Id)}: '{entity.Id}'.");
    }

    public async Task EnsurePartyExists(Politician entity, CancellationToken cancellationToken)
    {
        if (await _context.Parties.AsNoTracking().SingleOrDefaultAsync(x => x.Id == entity.PartyId, cancellationToken) is null)
            throw new InvalidRequestException($"{nameof(Party)} not found with {nameof(Party.Id)}: '{entity.PartyId}'.");
    }

    public async Task EnsureOnlyOnePartyLeader(Politician entity, CancellationToken cancellationToken)
    {
        if (!entity.IsPartyLeader) return;
        if (await _context.Politicians.AsNoTracking().AnyAsync(x => x.Party.Id == entity.PartyId && x.IsPartyLeader, cancellationToken))
            throw new InvalidEntityException($"{nameof(Politician)} can't be assigned {nameof(Politician.IsPartyLeader)} as its already assigned for {nameof(Politician.PartyId)}: '{entity.PartyId}'.");
    }
}