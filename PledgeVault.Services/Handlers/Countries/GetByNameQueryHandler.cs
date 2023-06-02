using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using PledgeVault.Core.Exceptions;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Persistence.Extensions;
using PledgeVault.Services.Queries;

namespace PledgeVault.Services.Handlers.Countries;

public sealed class GetByNameQueryHandler : IRequestHandler<GetByNameQuery<CountryResponse>, PageResponse<CountryResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByNameQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PageResponse<CountryResponse>> Handle(GetByNameQuery<CountryResponse> query, CancellationToken cancellationToken)
    {
        if (String.IsNullOrWhiteSpace(query.Name)) throw new InvalidRequestException();

        return new PageResponse<CountryResponse>
        {
            Data = await _context.Countries
                .AsNoTracking()
                .Paginate(query.Page)
                .Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{query.Name.ToLower()}%"))
                .ProjectTo<CountryResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken),
            PageNumber = query.Page.PageNumber,
            PageSize = query.Page.PageSize,
            TotalItems = await _context.Countries.CountAsync(cancellationToken)
        };
    }
}