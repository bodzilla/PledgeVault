using PledgeVault.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts.Entities.Validators;

/// <summary>
/// <inheritdoc cref="IEntityValidator{T}"/>
/// </summary>
public interface ICommentEntityValidator : IEntityValidator<Comment>
{
    Task EnsureUserExists(Comment entity, CancellationToken cancellationToken);

    Task EnsurePledgeExists(Comment entity, CancellationToken cancellationToken);
}