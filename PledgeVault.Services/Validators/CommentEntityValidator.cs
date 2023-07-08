using PledgeVault.Core.Contracts.Entities.Validators;
using PledgeVault.Core.Enums;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Validators;

/// <summary>
/// <inheritdoc cref="ICommentEntityValidator"/>
/// </summary>
public sealed class CommentEntityValidator : ICommentEntityValidator
{
    private readonly PledgeVaultContext _context;

    public CommentEntityValidator(PledgeVaultContext context) => _context = context;

    public async Task ValidateAllRules(EntityValidatorType type, Comment entity, CancellationToken cancellationToken)
    {
        if (type is not EntityValidatorType.Add) await EnsureEntityExists(entity, cancellationToken);
        await EnsureUserExists(entity, cancellationToken);
        await EnsurePledgeExists(entity, cancellationToken);
    }

    public async Task EnsureEntityExists(Comment entity, CancellationToken cancellationToken)
        => await CommonEntityValidator.EnsureCommentExists(_context, entity.Id, cancellationToken);

    public async Task EnsureUserExists(Comment entity, CancellationToken cancellationToken)
        => await CommonEntityValidator.EnsureUserExists(_context, entity.UserId, cancellationToken);

    public async Task EnsurePledgeExists(Comment entity, CancellationToken cancellationToken)
        => await CommonEntityValidator.EnsurePledgeExists(_context, entity.PledgeId, cancellationToken);
}