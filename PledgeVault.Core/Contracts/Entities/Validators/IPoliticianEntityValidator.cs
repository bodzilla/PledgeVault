using PledgeVault.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts.Entities.Validators;

/// <summary>
/// <inheritdoc cref="IEntityValidator{T}"/>
/// </summary>
public interface IPoliticianEntityValidator : IEntityValidator<Politician>
{
    Task EnsurePoliticianExists(Politician entity, CancellationToken cancellationToken);

    Task EnsurePartyExists(Politician entity, CancellationToken cancellationToken);

    Task EnsureOnlyOnePartyLeader(Politician entity, CancellationToken cancellationToken);
}