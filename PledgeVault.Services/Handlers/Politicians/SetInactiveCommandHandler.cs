﻿using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Politicians;

internal sealed class SetInactiveCommandHandler : IRequestHandler<SetInactiveCommand<PoliticianResponse>, PoliticianResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public SetInactiveCommandHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PoliticianResponse> Handle(SetInactiveCommand<PoliticianResponse> command, CancellationToken cancellationToken)
    {
        if (command.Id <= 0) throw new InvalidRequestException();

        var entity = await _context.Politicians.FindAsync(new object[] { command.Id }, cancellationToken) ?? throw new NotFoundException();

        entity.IsPartyLeader = false;
        entity.Position = null;
        entity.IsEntityActive = false;
        entity.EntityModified = DateTime.Now;
        _context.Politicians.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        //TODO: Do we want to set pledges/resources inactive also?

        return _mapper.Map<PoliticianResponse>(entity);
    }
}