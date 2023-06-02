using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using PledgeVault.Services.Queries.Politicians;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Politicians;

public sealed class GetAllQueryHandler : IRequestHandler<GetAllQuery, Page<PoliticianResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetAllQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Page<PoliticianResponse>> Handle(GetAllQuery query, CancellationToken cancellationToken)
        => new()
        {
            Data = await _context.Politicians
                .AsNoTracking()
                .PaginateFrom(query.PageOptions)
                .ProjectTo<PoliticianResponse>(_mapper.ConfigurationProvider, cancellationToken)
                .ToListAsync(cancellationToken),
            PageNumber = query.PageOptions.PageNumber,
            PageSize = query.PageOptions.PageSize,
            TotalItems = await _context.Countries.CountAsync(cancellationToken)
        };
}