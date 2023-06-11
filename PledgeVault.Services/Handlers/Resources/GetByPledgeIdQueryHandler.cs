using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using PledgeVault.Services.Queries.Resources;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Resources;

internal sealed class GetByPledgeIdQueryHandler : IRequestHandler<GetByPledgeIdQuery, Page<ResourceResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByPledgeIdQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Page<ResourceResponse>> Handle(GetByPledgeIdQuery query, CancellationToken cancellationToken)
    {
        if (query.Id <= 0) throw new InvalidRequestException();

        return new()
        {
            Data = await _context.Resources
                .AsNoTracking()
                .WithOnlyActiveEntities()
                .Where(x => x.PledgeId == query.Id)
                .WithPagination(query.PageOptions)
                .ProjectTo<ResourceResponse>(_mapper.ConfigurationProvider, cancellationToken)
                .ToListAsync(cancellationToken),
            PageNumber = query.PageOptions.PageNumber,
            PageSize = query.PageOptions.PageSize,
            TotalItems = await _context.Resources.CountAsync(cancellationToken)
        };
    }
}