using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Persistence;
using PledgeVault.Services.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Countries;

public sealed class GetByCountryIdQueryHandler : IRequestHandler<GetByIdQuery<CountryResponse>, CountryResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByCountryIdQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CountryResponse> Handle(GetByIdQuery<CountryResponse> query, CancellationToken cancellationToken)
    {
        if (query.Id <= 0) throw new InvalidRequestException();

        return await _context.Countries
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
            .ProjectTo<CountryResponse>(_mapper.ConfigurationProvider, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);
    }
}