using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts.Entities.Validators;
using PledgeVault.Core.Enums;
using PledgeVault.Core.Exceptions;
using PledgeVault.Core.Models;
using PledgeVault.Core.Security;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Validators;

/// <summary>
/// <inheritdoc cref="IUserEntityValidator"/>
/// </summary>
public sealed class UserEntityValidator : IUserEntityValidator
{
    private readonly PledgeVaultContext _context;

    public UserEntityValidator(PledgeVaultContext context) => _context = context;

    public async Task ValidateAllRules(EntityValidatorType type, User entity, CancellationToken cancellationToken)
    {
        if (type is not EntityValidatorType.Add) await EnsureEntityExists(entity, cancellationToken);
        await EnsureEmailIsUnique(entity, cancellationToken);
        await EnsureUsernameIsUnique(entity, cancellationToken);
    }

    public async Task EnsureEntityExists(User entity, CancellationToken cancellationToken)
        => await EntityValidator.EnsureCommentExists(_context, entity.Id, cancellationToken);

    public async Task EnsureEmailIsUnique(User entity, CancellationToken cancellationToken)
    {
        if (await _context.Users
                .AsNoTracking()
                .WithOnlyActiveEntities()
                .AnyAsync(x => EF.Functions.Like(x.Email, entity.Email), cancellationToken))
            throw new EntityValidationException($"{nameof(User.Email)} already in use.");
    }

    public async Task EnsureUsernameIsUnique(User entity, CancellationToken cancellationToken)
    {
        if (await _context.Users
                .AsNoTracking()
                .WithOnlyActiveEntities()
                .AnyAsync(x => EF.Functions.Like(x.Username, entity.Username), cancellationToken))
            throw new EntityValidationException($"{nameof(User.Username)} already in use.");
    }

    public void EnsureUsernameMeetsMinimumRequirements(string username) => AuthenticationManager.ValidateUsernameMeetsMinimumRequirements(username);

    public void EnsureRawPasswordMeetsMinimumRequirements(string password) => AuthenticationManager.ValidateRawPasswordMeetsMinimumRequirements(password);
}