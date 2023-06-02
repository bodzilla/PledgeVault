using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using PledgeVault.Services.Queries.Parties;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Parties;

public sealed class GetByCountryIdQueryHandler : IRequestHandler<GetByCountryIdQuery, Page<PartyResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByCountryIdQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Page<PartyResponse>> Handle(GetByCountryIdQuery query, CancellationToken cancellationToken)
        => new()
        {
            Data = await _context.Parties
                .AsNoTracking()
                .PaginateFrom(query.PageOptions)
                .Where(x => x.CountryId == query.Id)
                .ProjectTo<PartyResponse>(_mapper.ConfigurationProvider, cancellationToken)
                .ToListAsync(cancellationToken),
            PageNumber = query.PageOptions.PageNumber,
            PageSize = query.PageOptions.PageSize,
            TotalItems = await _context.Countries.CountAsync(cancellationToken)
        };
}