using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands.Politicians;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Politicians;

public sealed class AddCommandHandler : IRequestHandler<AddCommand, PoliticianResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public AddCommandHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PoliticianResponse> Handle(AddCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Politician>(command.Request);
        await _context.Politicians.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PoliticianResponse>(entity);
    }
}