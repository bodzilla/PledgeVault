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

namespace PledgeVault.Services.Handlers.Resources;

public sealed class UpdateCommandHandler : IRequestHandler<UpdateCommand<UpdateResourceRequest, ResourceResponse>, ResourceResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public UpdateCommandHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResourceResponse> Handle(UpdateCommand<UpdateResourceRequest, ResourceResponse> command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Resource>(command.Request);
        entity.EntityModified = DateTime.Now;
        _context.Resources.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ResourceResponse>(entity);
    }
}