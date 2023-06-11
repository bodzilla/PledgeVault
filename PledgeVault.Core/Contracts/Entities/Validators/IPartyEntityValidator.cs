using PledgeVault.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts.Entities.Validators;

/// <summary>
/// <inheritdoc cref="IEntityValidator{T}"/>
/// </summary>
public interface IPartyEntityValidator : IEntityValidator<Party>
{
    Task EnsureNameWithCountryIdIsUnique(Party entity, CancellationToken cancellationToken);

    Task EnsureCountryExists(Party entity, CancellationToken cancellationToken);
}