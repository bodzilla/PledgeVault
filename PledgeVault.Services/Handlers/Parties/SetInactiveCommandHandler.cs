using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Parties;

internal sealed class SetInactiveCommandHandler : IRequestHandler<SetInactiveCommand<PartyResponse>, PartyResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public SetInactiveCommandHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PartyResponse> Handle(SetInactiveCommand<PartyResponse> command, CancellationToken cancellationToken)
    {
        if (command.Id <= 0) throw new InvalidRequestException();

        var entity = await _context.Parties.FindAsync(new object[] { command.Id }, cancellationToken) ?? throw new NotFoundException();

        entity.EntityActive = false;
        entity.EntityModified = DateTime.Now;
        _context.Parties.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PartyResponse>(entity);
    }
}