using PledgeVault.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts.Entities.Validators;

/// <summary>
/// <inheritdoc cref="IEntityValidator{T}"/>
/// </summary>
public interface IPledgeEntityValidator : IEntityValidator<Pledge>
{
    Task EnsureUserExists(Pledge entity, CancellationToken cancellationToken);

    Task EnsurePoliticianExists(Pledge entity, CancellationToken cancellationToken);

    Task EnsureTitleWithDatePledgedWithPoliticianIdIsUnique(Pledge entity, CancellationToken cancellationToken);
}