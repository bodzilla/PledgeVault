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

namespace PledgeVault.Services.Handlers.Resources;

internal sealed class GetAllQueryHandler : IRequestHandler<GetAllQuery<ResourceResponse>, Page<ResourceResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetAllQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Page<ResourceResponse>> Handle(GetAllQuery<ResourceResponse> query, CancellationToken cancellationToken)
    {
        var baseQuery = _context.Resources
            .AsNoTracking()
            .WithOnlyActiveEntities();

        return new Page<ResourceResponse>
        {
            Data = await baseQuery
                .WithPagination(query.PageOptions)
                .ProjectTo<ResourceResponse>(_mapper.ConfigurationProvider, cancellationToken, x => x.Pledge)
                .ToListAsync(cancellationToken),
            PageNumber = query.PageOptions.PageNumber,
            PageSize = query.PageOptions.PageSize,
            TotalItems = await baseQuery.CountAsync(cancellationToken)
        };
    }
}