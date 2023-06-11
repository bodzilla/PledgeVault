using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts.Entities.Validators;

/// <summary>
/// All entity validators must implement <see cref="IEntityValidator{T}"/>. If the entity validator is injecting a DbContext,
/// it should be registered as Scoped and ensure all DB operations are handled as "AsNoTracking(Async)"
/// to avoid conflicts with the Mediator handler's DbContext.
/// </summary>
public interface IEntityValidator<in T> where T : IEntity
{
    Task ValidateAllRules(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Ensures that the entity exists and <see cref="IEntity.EntityActive"/> is true.
    /// </summary>
    Task EnsureEntityExists(T entity, CancellationToken cancellationToken);
}