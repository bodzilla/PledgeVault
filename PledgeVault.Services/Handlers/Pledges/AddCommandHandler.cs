using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands.Pledges;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Pledges;

public sealed class AddCommandHandler : IRequestHandler<AddCommand, PledgeResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public AddCommandHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PledgeResponse> Handle(AddCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Pledge>(command.Request);
        await _context.Pledges.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PledgeResponse>(entity);
    }
}