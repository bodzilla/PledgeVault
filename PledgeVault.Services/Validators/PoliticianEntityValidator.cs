﻿using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts.Entities;
using PledgeVault.Core.Exceptions;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Validators;

public sealed class PoliticianEntityValidator : IPoliticianEntityValidator
{
    private readonly PledgeVaultContext _context;

    public PoliticianEntityValidator(PledgeVaultContext context) => _context = context;

    public async Task ValidateAllRules(Politician request, CancellationToken cancellationToken)
        => await EnsureOnlyOnePartyLeader(request, cancellationToken);

    public async Task EnsureOnlyOnePartyLeader(Politician entity, CancellationToken cancellationToken)
    {
        if (!entity.IsPartyLeader) return;
        if (await _context.Politicians.AnyAsync(x => x.Party.Id == entity.PartyId && x.IsPartyLeader, cancellationToken))
            throw new InvalidEntityException($"Cannot assign entity as {nameof(Politician.IsPartyLeader)} as its already assigned for {nameof(Politician.PartyId)}: '{entity.PartyId}'.");
    }
}