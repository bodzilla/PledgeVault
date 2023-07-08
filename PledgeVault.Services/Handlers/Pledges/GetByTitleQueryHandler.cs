using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using PledgeVault.Services.Queries.Pledges;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Pledges;

internal sealed class GetByTitleQueryHandler : IRequestHandler<GetByTitleQuery, Page<PledgeResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByTitleQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Page<PledgeResponse>> Handle(GetByTitleQuery query, CancellationToken cancellationToken)
    {
        if (String.IsNullOrWhiteSpace(query.Title)) throw new InvalidRequestException();

        var baseQuery = _context.Pledges
            .AsNoTracking()
            .WithOnlyActiveEntities()
            .Where(x => EF.Functions.Like(x.Title, $"%{query.Title}%"));

        return new Page<PledgeResponse>
        {
            Data = await baseQuery
                .WithPagination(query.PageOptions)
                .ProjectTo<PledgeResponse>(_mapper.ConfigurationProvider, cancellationToken,
                    x => x.User, x => x.Politician, x => x.Resources)
                .ToListAsync(cancellationToken),
            PageNumber = query.PageOptions.PageNumber,
            PageSize = query.PageOptions.PageSize,
            TotalItems = await baseQuery.CountAsync(cancellationToken)
        };
    }
}