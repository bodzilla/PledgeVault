using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts.Services;
using PledgeVault.Core.Exceptions;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services;

public sealed class PoliticianService : IPoliticianService
{
    private readonly PledgeVaultContext _context;

    public PoliticianService(PledgeVaultContext context) => _context = context;

    public async Task CheckPartyLeaderAssignedAsync(Politician entity, CancellationToken cancellationToken)
    {
        if (!entity.IsPartyLeader) return;
        if (await _context.Politicians.AnyAsync(x => x.Party.Id == entity.PartyId && x.IsPartyLeader, cancellationToken))
            throw new InvalidRequestException($"Cannot assign as {nameof(Politician.IsPartyLeader)} as its already assigned for {nameof(Politician.PartyId)}: '{entity.PartyId}'.");
    }
}