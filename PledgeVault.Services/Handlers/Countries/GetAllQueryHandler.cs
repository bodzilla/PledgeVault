using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using PledgeVault.Services.Queries.Countries;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Countries;

public sealed class GetAllQueryHandler : IRequestHandler<GetAllQuery, Page<CountryResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetAllQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Page<CountryResponse>> Handle(GetAllQuery query, CancellationToken cancellationToken)
        => new()
        {
            Data = await _context.Countries
                .AsNoTracking()
                .PaginateFrom(query.PageOptions)
                .ProjectTo<CountryResponse>(_mapper.ConfigurationProvider, cancellationToken)
                .ToListAsync(cancellationToken),
            PageNumber = query.PageOptions.PageNumber,
            PageSize = query.PageOptions.PageSize,
            TotalItems = await _context.Countries.CountAsync(cancellationToken)
        };
}