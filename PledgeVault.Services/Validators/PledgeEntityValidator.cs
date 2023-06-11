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
/// <inheritdoc cref="IPledgeEntityValidator"/>
/// </summary>
public sealed class PledgeEntityValidator : IPledgeEntityValidator
{
    private readonly PledgeVaultContext _context;

    public PledgeEntityValidator(PledgeVaultContext context) => _context = context;

    public async Task ValidateAllRules(EntityValidatorType type, Pledge entity, CancellationToken cancellationToken)
    {
        if (type is not EntityValidatorType.Add) await EnsureEntityExists(entity, cancellationToken);
        await EnsurePoliticianExists(entity, cancellationToken);
        await EnsureTitleWithDatePledgedWithPoliticianIdIsUnique(entity, cancellationToken);
    }

    public async Task EnsureEntityExists(Pledge entity, CancellationToken cancellationToken)
    {
        if (await _context.Pledges
                .AsNoTracking()
                .WithOnlyActiveEntities()
                .SingleOrDefaultAsync(x => x.Id == entity.Id, cancellationToken) is null)
            throw new InvalidEntityException($"{nameof(Party)} not found with {nameof(Party.Id)}: '{entity.Id}'.");
    }

    public async Task EnsurePoliticianExists(Pledge entity, CancellationToken cancellationToken)
    {
        if (await _context.Politicians
                .AsNoTracking()
                .WithOnlyActiveEntities()
                .SingleOrDefaultAsync(x => x.Id == entity.PoliticianId, cancellationToken) is null)
            throw new InvalidEntityException($"{nameof(Politician)} not found with {nameof(Politician.Id)}: '{entity.PoliticianId}'.");
    }

    public async Task EnsureTitleWithDatePledgedWithPoliticianIdIsUnique(Pledge entity, CancellationToken cancellationToken)
    {
        if (await _context.Pledges
                .AsNoTracking()
                .WithOnlyActiveEntities()
                .SingleOrDefaultAsync(x => EF.Functions.Like(x.Title, entity.Title) && x.DatePledged == entity.DatePledged && x.PoliticianId == entity.PoliticianId, cancellationToken) is not null)
            throw new InvalidEntityException($"{nameof(Pledge.Title)} with {nameof(Pledge.DatePledged)} with {nameof(Pledge.PoliticianId)} already exists.");
    }
}