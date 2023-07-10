using AutoMapper;
using MediatR;
using PledgeVault.Core.Contracts.Entities.Validators;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Core.Models;
using PledgeVault.Core.Security;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands.Users;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Users;

internal sealed class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, UserResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IUserEntityValidator _entityValidator;
    private readonly IMapper _mapper;

    public UpdateUserPasswordCommandHandler(PledgeVaultContext context, IUserEntityValidator entityValidator, IMapper mapper)
    {
        _context = context;
        _entityValidator = entityValidator;
        _mapper = mapper;
    }

    public async Task<UserResponse> Handle(UpdateUserPasswordCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Users.FindAsync(new object[] { command.Request.Id, cancellationToken }, cancellationToken)
                     ?? throw new NotFoundException($"{nameof(User)} not found with {nameof(command.Request.Id)}: '{command.Request.Id}'.");

        if (!AuthenticationManager.IsPasswordMatch(command.Request.CurrentPassword, entity.HashedPassword)) throw new AuthenticationException();

        _entityValidator.EnsureRawPasswordMeetsMinimumRequirements(command.Request.NewPassword);

        // Only map the changes to the entity.
        _mapper.Map(command.Request, entity);

        entity.EntityModified = DateTime.Now;
        _context.Users.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UserResponse>(entity);
    }
}