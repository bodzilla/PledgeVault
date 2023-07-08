using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using PledgeVault.Services.Queries.Comments;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Comments;

internal sealed class GetByPledgeIdQueryHandler : IRequestHandler<GetByPledgeIdQuery, Page<CommentResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByPledgeIdQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Page<CommentResponse>> Handle(GetByPledgeIdQuery query, CancellationToken cancellationToken)
    {
        var baseQuery = _context.Comments
            .AsNoTracking()
            .WithOnlyActiveEntities()
            .Where(x => x.PledgeId == query.Id);

        return new Page<CommentResponse>
        {
            Data = await baseQuery
                .WithPagination(query.PageOptions)
                .ProjectTo<CommentResponse>(_mapper.ConfigurationProvider, cancellationToken)
                .ToListAsync(cancellationToken),
            PageNumber = query.PageOptions.PageNumber,
            PageSize = query.PageOptions.PageSize,
            TotalItems = await baseQuery.CountAsync(cancellationToken)
        };
    }
}