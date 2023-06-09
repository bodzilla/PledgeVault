using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
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
    private readonly IMapper _mapper;

    public UpdateCommandHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PledgeResponse> Handle(UpdateCommand<UpdatePledgeRequest, PledgeResponse> command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Pledge>(command.Request);
        entity.EntityModified = DateTime.Now;
        _context.Pledges.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PledgeResponse>(entity);
    }
}