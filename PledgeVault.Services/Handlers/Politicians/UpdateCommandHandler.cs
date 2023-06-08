using AutoMapper;
using MediatR;
using PledgeVault.Core.Contracts.Services;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Politicians;

public sealed class UpdateCommandHandler : IRequestHandler<UpdateCommand<UpdatePoliticianRequest, PoliticianResponse>, PoliticianResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IPoliticianService _service;
    private readonly IMapper _mapper;

    public UpdateCommandHandler(PledgeVaultContext context, IPoliticianService service, IMapper mapper)
    {
        _context = context;
        _service = service;
        _mapper = mapper;
    }

    public async Task<PoliticianResponse> Handle(UpdateCommand<UpdatePoliticianRequest, PoliticianResponse> command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Politician>(command.Request);
        await _service.CheckPartyLeaderAssignedAsync(entity, cancellationToken);
        entity.EntityModified = DateTime.Now;
        _context.Politicians.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PoliticianResponse>(entity);
    }
}