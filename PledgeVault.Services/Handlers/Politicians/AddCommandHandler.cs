using AutoMapper;
using MediatR;
using PledgeVault.Core.Contracts.Services;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Politicians;

public sealed class AddCommandHandler : IRequestHandler<AddCommand<AddPoliticianRequest, PoliticianResponse>, PoliticianResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IPoliticianService _service;
    private readonly IMapper _mapper;

    public AddCommandHandler(PledgeVaultContext context, IPoliticianService service, IMapper mapper)
    {
        _context = context;
        _service = service;
        _mapper = mapper;
    }

    public async Task<PoliticianResponse> Handle(AddCommand<AddPoliticianRequest, PoliticianResponse> command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Politician>(command.Request);
        await _service.CheckIfPartyLeaderExists(entity, cancellationToken);
        await _context.Politicians.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PoliticianResponse>(entity);
    }
}