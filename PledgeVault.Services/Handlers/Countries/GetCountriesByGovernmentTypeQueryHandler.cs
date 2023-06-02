using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using PledgeVault.Services.Queries.Countries;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Pagination;

namespace PledgeVault.Services.Handlers.Countries;

public sealed class GetCountriesByGovernmentTypeQueryHandler : IRequestHandler<GetCountriesByGovernmentTypeQuery, PaginationResponse<CountryResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetCountriesByGovernmentTypeQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<CountryResponse>> Handle(GetCountriesByGovernmentTypeQuery query, CancellationToken cancellationToken)
        => new()
        {
            Data = await _context.Countries
                .AsNoTracking()
                .Skip((query.PaginationQuery.PageNumber - 1) * query.PaginationQuery.PageSize)
                .Take(query.PaginationQuery.PageSize)
                .Where(x => x.GovernmentType == query.GovernmentType)
                .ProjectTo<CountryResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken),
            PageNumber = query.PaginationQuery.PageNumber,
            PageSize = query.PaginationQuery.PageSize,
            TotalItems = await _context.Countries.CountAsync(cancellationToken)
        };
}