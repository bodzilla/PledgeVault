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
        await EnsureUserExists(entity, cancellationToken);
        await EnsurePoliticianExists(entity, cancellationToken);
        await EnsureTitleWithDatePledgedWithPoliticianIdIsUnique(entity, cancellationToken);
    }

    public async Task EnsureEntityExists(Pledge entity, CancellationToken cancellationToken)
        => await CommonEntityValidator.EnsurePledgeExists(_context, entity.Id, cancellationToken);

    public async Task EnsureUserExists(Pledge entity, CancellationToken cancellationToken)
        => await CommonEntityValidator.EnsureUserExists(_context, entity.UserId, cancellationToken);

    public async Task EnsurePoliticianExists(Pledge entity, CancellationToken cancellationToken)
        => await CommonEntityValidator.EnsurePoliticianExists(_context, entity.PoliticianId, cancellationToken);

    public async Task EnsureTitleWithDatePledgedWithPoliticianIdIsUnique(Pledge entity, CancellationToken cancellationToken)
    {
        if (await _context.Pledges
                .AsNoTracking()
                .WithOnlyActiveEntities()
                .SingleOrDefaultAsync(x => EF.Functions.Like(x.Title, entity.Title) && x.DatePledged == entity.DatePledged && x.PoliticianId == entity.PoliticianId, cancellationToken) is not null)
            throw new EntityValidationException($"{nameof(Pledge.Title)} with {nameof(Pledge.DatePledged)} with {nameof(Pledge.PoliticianId)} already exists.");
    }
}