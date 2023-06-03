using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Resources;

public sealed class AddCommandHandler : IRequestHandler<AddCommand<AddResourceRequest, ResourceResponse>, ResourceResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public AddCommandHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResourceResponse> Handle(AddCommand<AddResourceRequest, ResourceResponse> command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Resource>(command.Request);
        await _context.Resources.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ResourceResponse>(entity);
    }
}