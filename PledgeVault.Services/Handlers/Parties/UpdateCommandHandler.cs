﻿using AutoMapper;
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

namespace PledgeVault.Services.Handlers.Parties;

internal sealed class UpdateCommandHandler : IRequestHandler<UpdateCommand<UpdatePartyRequest, PartyResponse>, PartyResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IPartyEntityValidator _entityValidator;
    private readonly IMapper _mapper;

    public UpdateCommandHandler(PledgeVaultContext context, IPartyEntityValidator entityValidator, IMapper mapper)
    {
        _context = context;
        _entityValidator = entityValidator;
        _mapper = mapper;
    }

    public async Task<PartyResponse> Handle(UpdateCommand<UpdatePartyRequest, PartyResponse> command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Party>(command.Request);
        await _entityValidator.ValidateAllRules(EntityValidatorType.Update, entity, cancellationToken);
        entity.EntityModified = DateTime.Now;
        _context.Parties.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PartyResponse>(entity);
    }
}