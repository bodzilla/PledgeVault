using PledgeVault.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts.Entities.Validators;

/// <summary>
/// <inheritdoc cref="IEntityValidator{T}"/>
/// </summary>
public interface IUserEntityValidator : IEntityValidator<User>
{
    Task EnsureEmailIsUnique(User entity, CancellationToken cancellationToken);

    Task EnsureUsernameIsUnique(User entity, CancellationToken cancellationToken);

    void EnsureUsernameMeetsMinimumRequirements(string username);

    void EnsureRawPasswordMeetsMinimumRequirements(string password);
}