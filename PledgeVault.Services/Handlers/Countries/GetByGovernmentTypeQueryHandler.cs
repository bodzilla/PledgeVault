using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using PledgeVault.Services.Queries.Countries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Countries;

internal sealed class GetByPartyIdQueryHandler : IRequestHandler<GetByGovernmentTypeQuery, Page<CountryResponse>>
{
    private readonly IDbContextFactory<PledgeVaultContext> _contextFactory;
    private readonly IMapper _mapper;

    public GetByPartyIdQueryHandler(IDbContextFactory<PledgeVaultContext> contextFactory, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<Page<CountryResponse>> Handle(GetByGovernmentTypeQuery query, CancellationToken cancellationToken)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var baseQuery = context.Countries
            .AsNoTracking()
            .WithOnlyActiveEntities()
            .Where(x => x.GovernmentType == query.GovernmentType);

        return new Page<CountryResponse>
        {
            Data = await baseQuery
                .WithPagination(query.PageOptions)
                .ProjectTo<CountryResponse>(_mapper.ConfigurationProvider, cancellationToken)
                .ToListAsync(cancellationToken),
            PageNumber = query.PageOptions.PageNumber,
            PageSize = query.PageOptions.PageSize,
            TotalItems = await baseQuery.CountAsync(cancellationToken)
        };
    }
}