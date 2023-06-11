using PledgeVault.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts.Entities.Validators;

/// <summary>
/// <inheritdoc cref="IEntityValidator{T}"/>
/// </summary>
public interface ICountryEntityValidator : IEntityValidator<Country>
{
    Task EnsureNameIsUnique(Country entity, CancellationToken cancellationToken);
}