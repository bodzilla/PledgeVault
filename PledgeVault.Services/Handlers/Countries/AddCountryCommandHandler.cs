using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using PledgeVault.Core.Models;
using PledgeVault.Services.Commands.Countries;
using System.Threading.Tasks;
using System.Threading;

namespace PledgeVault.Services.Handlers.Countries;

public sealed class AddCountryCommandHandler : IRequestHandler<AddCountryCommand, CountryResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public AddCountryCommandHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CountryResponse> Handle(AddCountryCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Country>(command.Request);
        await _context.Countries.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CountryResponse>(entity);
    }
}