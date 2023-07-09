using AutoMapper;
using MediatR;
using PledgeVault.Core.Contracts.Entities.Validators;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands.Users;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Users;

internal sealed class UpdateUserEmailCommandHandler : IRequestHandler<UpdateUserEmailCommand, UserResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IUserEntityValidator _entityValidator;
    private readonly IMapper _mapper;

    public UpdateUserEmailCommandHandler(PledgeVaultContext context, IUserEntityValidator entityValidator, IMapper mapper)
    {
        _context = context;
        _entityValidator = entityValidator;
        _mapper = mapper;
    }

    public async Task<UserResponse> Handle(UpdateUserEmailCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Users.FindAsync(new object[] { command.Request.Id, cancellationToken }, cancellationToken)
                     ?? throw new NotFoundException($"{nameof(User)} not found with {nameof(command.Request.Id)}: '{command.Request.Id}'.");

        // Only map the changes to the entity.
        _mapper.Map(command.Request, entity);

        await _entityValidator.EnsureEmailIsUnique(entity, cancellationToken);
        entity.EntityModified = DateTime.Now;
        _context.Users.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UserResponse>(entity);
    }
}