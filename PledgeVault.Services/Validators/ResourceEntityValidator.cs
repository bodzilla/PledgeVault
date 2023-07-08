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
/// <inheritdoc cref="IResourceEntityValidator"/>
/// </summary>
public sealed class ResourceEntityValidator : IResourceEntityValidator
{
    private readonly PledgeVaultContext _context;

    public ResourceEntityValidator(PledgeVaultContext context) => _context = context;

    public async Task ValidateAllRules(EntityValidatorType type, Resource entity, CancellationToken cancellationToken)
    {
        if (type is not EntityValidatorType.Add) await EnsureEntityExists(entity, cancellationToken);
        await EnsureUserExists(entity, cancellationToken);
        await EnsurePledgeExists(entity, cancellationToken);
        await EnsureSiteUrlWithPledgeIdIsUnique(entity, cancellationToken);
    }

    public async Task EnsureEntityExists(Resource entity, CancellationToken cancellationToken)
        => await EntityValidator.EnsureResourceExists(_context, entity.Id, cancellationToken);

    public async Task EnsureUserExists(Resource entity, CancellationToken cancellationToken)
        => await EntityValidator.EnsureUserExists(_context, entity.UserId, cancellationToken);

    public async Task EnsurePledgeExists(Resource entity, CancellationToken cancellationToken)
        => await EntityValidator.EnsurePledgeExists(_context, entity.PledgeId, cancellationToken);

    public async Task EnsureSiteUrlWithPledgeIdIsUnique(Resource entity, CancellationToken cancellationToken)
    {
        if (await _context.Resources
                .AsNoTracking()
                .WithOnlyActiveEntities()
                .AnyAsync(x => EF.Functions.Like(x.SiteUrl, entity.SiteUrl) && x.PledgeId == entity.PledgeId, cancellationToken))
            throw new EntityValidationException($"{nameof(Resource.SiteUrl)} with  {nameof(Resource.PledgeId)} already exists.");
    }
}