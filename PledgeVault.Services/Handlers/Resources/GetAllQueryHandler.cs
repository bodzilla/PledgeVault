using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using System.Threading;
using System.Threading.Tasks;
using PledgeVault.Services.Queries.Resources;

namespace PledgeVault.Services.Handlers.Resources;

public sealed class GetAllQueryHandler : IRequestHandler<GetAllQuery, Page<ResourceResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetAllQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Page<ResourceResponse>> Handle(GetAllQuery query, CancellationToken cancellationToken)
        => new()
        {
            Data = await _context.Resources
                .AsNoTracking()
                .PaginateFrom(query.PageOptions)
                .ProjectTo<ResourceResponse>(_mapper.ConfigurationProvider, cancellationToken)
                .ToListAsync(cancellationToken),
            PageNumber = query.PageOptions.PageNumber,
            PageSize = query.PageOptions.PageSize,
            TotalItems = await _context.Resources.CountAsync(cancellationToken)
        };
}