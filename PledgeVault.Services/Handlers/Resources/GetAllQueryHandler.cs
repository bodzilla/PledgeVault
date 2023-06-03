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

namespace PledgeVault.Services.Handlers.Resources;

public sealed class GetAllQueryHandler : IRequestHandler<GetAllQuery<Resource, ResourceResponse>, Page<ResourceResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetAllQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Page<ResourceResponse>> Handle(GetAllQuery<Resource, ResourceResponse> query, CancellationToken cancellationToken)
    {
        var dbSet = _context.Set<Resource>();

        return new()
        {
            Data = await dbSet
                .AsNoTracking()
                .PaginateFrom(query.PageOptions)
                .ProjectTo<ResourceResponse>(_mapper.ConfigurationProvider, cancellationToken, x => x.Pledge)
                .ToListAsync(cancellationToken),
            PageNumber = query.PageOptions.PageNumber,
            PageSize = query.PageOptions.PageSize,
            TotalItems = await dbSet.CountAsync(cancellationToken)
        };
    }
}