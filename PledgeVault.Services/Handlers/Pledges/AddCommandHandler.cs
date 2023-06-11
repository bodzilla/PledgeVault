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

namespace PledgeVault.Services.Handlers.Pledges;

internal sealed class AddCommandHandler : IRequestHandler<AddCommand<AddPledgeRequest, PledgeResponse>, PledgeResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IPledgeEntityValidator _entityValidator;
    private readonly IMapper _mapper;

    public AddCommandHandler(PledgeVaultContext context, IPledgeEntityValidator entityValidator, IMapper mapper)
    {
        _context = context;
        _entityValidator = entityValidator;
        _mapper = mapper;
    }

    public async Task<PledgeResponse> Handle(AddCommand<AddPledgeRequest, PledgeResponse> command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Pledge>(command.Request);
        await _entityValidator.ValidateAllRules(EntityValidatorType.Add, entity, cancellationToken);
        await _context.Pledges.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PledgeResponse>(entity);
    }
}