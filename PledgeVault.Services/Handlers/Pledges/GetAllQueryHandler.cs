using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using PledgeVault.Services.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Pledges;

internal sealed class GetAllQueryHandler : IRequestHandler<GetAllQuery<PledgeResponse>, Page<PledgeResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetAllQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Page<PledgeResponse>> Handle(GetAllQuery<PledgeResponse> query, CancellationToken cancellationToken)
    {
        var dbSet = _context.Set<Pledge>();

        return new()
        {
            Data = await dbSet
                .AsNoTracking()
                .WithPagination(query.PageOptions)
                .ProjectTo<PledgeResponse>(_mapper.ConfigurationProvider, cancellationToken, x => x.Resources)
                .ToListAsync(cancellationToken),
            PageNumber = query.PageOptions.PageNumber,
            PageSize = query.PageOptions.PageSize,
            TotalItems = await dbSet.CountAsync(cancellationToken)
        };
    }
}