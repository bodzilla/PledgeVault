using AutoMapper;
using MediatR;
using PledgeVault.Core.Contracts.Entities.Validators;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Enums;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Users;

internal sealed class AddCommandHandler : IRequestHandler<AddCommand<AddUserRequest, UserResponse>, UserResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IUserEntityValidator _entityValidator;
    private readonly IMapper _mapper;

    public AddCommandHandler(PledgeVaultContext context, IUserEntityValidator entityValidator, IMapper mapper)
    {
        _context = context;
        _entityValidator = entityValidator;
        _mapper = mapper;
    }

    public async Task<UserResponse> Handle(AddCommand<AddUserRequest, UserResponse> command, CancellationToken cancellationToken)
    {
        // Validate these first to avoid potentially unnecessary database calls.
        _entityValidator.EnsureUsernameMeetsMinimumRequirements(command.Request.Username);
        _entityValidator.EnsureRawPasswordMeetsMinimumRequirements(command.Request.Password);

        var entity = _mapper.Map<User>(command.Request);
        await _entityValidator.ValidateAllRules(EntityValidatorType.Add, entity, cancellationToken);
        await _context.Users.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UserResponse>(entity);
    }
}