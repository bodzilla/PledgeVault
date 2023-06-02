﻿using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands.Parties;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Parties;

public sealed class UpdateCommandHandler : IRequestHandler<UpdateCommand, PartyResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public UpdateCommandHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PartyResponse> Handle(UpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Party>(command.Request);
        entity.EntityModified = DateTime.Now;
        _context.Parties.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PartyResponse>(entity);
    }
}