using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands.Politicians;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Politicians;

public sealed class UpdateCommandHandler : IRequestHandler<UpdateCommand, PoliticianResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public UpdateCommandHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PoliticianResponse> Handle(UpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Politician>(command.Request);
        entity.EntityModified = DateTime.Now;
        _context.Politicians.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PoliticianResponse>(entity);
    }
}