﻿using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Pledges;

internal sealed class SetInactiveCommandHandler : IRequestHandler<SetInactiveCommand<PledgeResponse>, PledgeResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public SetInactiveCommandHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PledgeResponse> Handle(SetInactiveCommand<PledgeResponse> command, CancellationToken cancellationToken)
    {
        if (command.Id <= 0) throw new InvalidRequestException();

        var entity = await _context.Pledges.FindAsync(new object[] { command.Id }, cancellationToken) ?? throw new NotFoundException();

        entity.IsEntityActive = false;
        entity.EntityModified = DateTime.Now;
        _context.Pledges.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PledgeResponse>(entity);
    }
}