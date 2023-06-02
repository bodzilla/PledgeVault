using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Services.Queries;

namespace PledgeVault.Services.Handlers.Parties;

public sealed class GetAllQueryHandler : IRequestHandler<GetAllQuery<PartyResponse>, PaginationResponse<PartyResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetAllQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<PartyResponse>> Handle(GetAllQuery<PartyResponse> query, CancellationToken cancellationToken)
        => new()
        {
            Data = await _context.Parties
                .AsNoTracking()
                .Skip((query.PaginationQuery.PageNumber - 1) * query.PaginationQuery.PageSize)
                .Take(query.PaginationQuery.PageSize)
                .ProjectTo<PartyResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken),
            PageNumber = query.PaginationQuery.PageNumber,
            PageSize = query.PaginationQuery.PageSize,
            TotalItems = await _context.Parties.CountAsync(cancellationToken)
        };
}