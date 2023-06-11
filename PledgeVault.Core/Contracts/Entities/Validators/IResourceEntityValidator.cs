using PledgeVault.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts.Entities.Validators;

/// <summary>
/// <inheritdoc cref="IEntityValidator{T}"/>
/// </summary>
public interface IResourceEntityValidator : IEntityValidator<Resource>
{
    Task EnsurePledgeExists(Resource entity, CancellationToken cancellationToken);

    Task EnsureSiteUrlWithPledgeIdIsUnique(Resource entity, CancellationToken cancellationToken);
}