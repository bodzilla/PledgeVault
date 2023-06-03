using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using PledgeVault.Services.Queries;
using System.Threading;
using System.Threading.Tasks;
using PledgeVault.Core.Models;

namespace PledgeVault.Services.Handlers.Countries;

public sealed class GetAllQueryHandler : IRequestHandler<GetAllQuery<Country, CountryResponse>, Page<CountryResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetAllQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Page<CountryResponse>> Handle(GetAllQuery<Country, CountryResponse> query, CancellationToken cancellationToken)
    {
        var dbSet = _context.Set<Country>();

        return new()
        {
            Data = await dbSet
                .AsNoTracking()
                .PaginateFrom(query.PageOptions)
                .ProjectTo<CountryResponse>(_mapper.ConfigurationProvider, cancellationToken)
                .ToListAsync(cancellationToken),
            PageNumber = query.PageOptions.PageNumber,
            PageSize = query.PageOptions.PageSize,
            TotalItems = await dbSet.CountAsync(cancellationToken)
        };
    }
}