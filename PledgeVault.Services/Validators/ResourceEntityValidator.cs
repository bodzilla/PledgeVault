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
        await EnsurePledgeExists(entity, cancellationToken);
        await EnsureSiteUrlWithPledgeIdIsUnique(entity, cancellationToken);
    }

    public async Task EnsureEntityExists(Resource entity, CancellationToken cancellationToken)
    {
        if (await _context.Resources
                .AsNoTracking()
                .WithOnlyActiveEntities()
                .SingleOrDefaultAsync(x => x.Id == entity.Id, cancellationToken) is null)
            throw new InvalidEntityException($"{nameof(Resource)} not found with {nameof(Resource.Id)}: '{entity.Id}'.");
    }

    public async Task EnsurePledgeExists(Resource entity, CancellationToken cancellationToken)
    {
        if (await _context.Pledges
                .AsNoTracking()
                .WithOnlyActiveEntities()
                .SingleOrDefaultAsync(x => x.Id == entity.PledgeId, cancellationToken) is null)
            throw new InvalidEntityException($"{nameof(Pledge)} not found with {nameof(Pledge.Id)}: '{entity.PledgeId}'.");
    }

    public async Task EnsureSiteUrlWithPledgeIdIsUnique(Resource entity, CancellationToken cancellationToken)
    {
        if (await _context.Resources
                .AsNoTracking()
                .WithOnlyActiveEntities()
                .SingleOrDefaultAsync(x => EF.Functions.Like(x.SiteUrl, entity.SiteUrl) && x.PledgeId == entity.PledgeId, cancellationToken) is not null)
            throw new InvalidEntityException($"{nameof(Resource.SiteUrl)} with  {nameof(Resource.PledgeId)} already exists.");
    }
}