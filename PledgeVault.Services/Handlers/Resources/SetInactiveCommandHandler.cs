using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Resources;

internal sealed class SetInactiveCommandHandler : IRequestHandler<SetInactiveCommand<ResourceResponse>, ResourceResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public SetInactiveCommandHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResourceResponse> Handle(SetInactiveCommand<ResourceResponse> command, CancellationToken cancellationToken)
    {
        if (command.Id <= 0) throw new InvalidRequestException();

        var entity = await _context.Resources.FindAsync(new object[] { command.Id }, cancellationToken) ?? throw new NotFoundException();

        entity.EntityActive = false;
        entity.EntityModified = DateTime.Now;
        _context.Resources.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ResourceResponse>(entity);
    }
}