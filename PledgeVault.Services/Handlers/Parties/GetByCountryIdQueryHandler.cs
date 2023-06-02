using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Services.Queries.Parties;
using PledgeVault.Core.Dtos.Pagination;

namespace PledgeVault.Services.Handlers.Parties;

public sealed class GetByCountryIdQueryHandler : IRequestHandler<GetByCountryIdQuery, PaginationResponse<PartyResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByCountryIdQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<PartyResponse>> Handle(GetByCountryIdQuery query, CancellationToken cancellationToken)
        => new()
        {
            Data = await _context.Parties
                .AsNoTracking()
                .Skip((query.PaginationQuery.PageNumber - 1) * query.PaginationQuery.PageSize)
                .Take(query.PaginationQuery.PageSize)
                .Where(x => x.CountryId == query.Id)
                .ProjectTo<PartyResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken),
            PageNumber = query.PaginationQuery.PageNumber,
            PageSize = query.PaginationQuery.PageSize,
            TotalItems = await _context.Countries.CountAsync(cancellationToken)
        };
}