using AutoMapper;
using MediatR;
using PledgeVault.Core.Contracts.Entities.Validators;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Enums;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Pledges;

internal sealed class UpdateCommandHandler : IRequestHandler<UpdateCommand<UpdatePledgeRequest, PledgeResponse>, PledgeResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IPledgeEntityValidator _entityValidator;
    private readonly IMapper _mapper;

    public UpdateCommandHandler(PledgeVaultContext context, IPledgeEntityValidator entityValidator, IMapper mapper)
    {
        _context = context;
        _entityValidator = entityValidator;
        _mapper = mapper;
    }

    public async Task<PledgeResponse> Handle(UpdateCommand<UpdatePledgeRequest, PledgeResponse> command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Pledge>(command.Request);
        await _entityValidator.ValidateAllRules(EntityValidatorType.Update, entity, cancellationToken);
        entity.EntityModified = DateTime.Now;
        _context.Pledges.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PledgeResponse>(entity);
    }
}