using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts;
using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using PledgeVault.Services.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers;

public sealed class GetAllQueryHandler<TEntity, TResponse> : IRequestHandler<GetAllQuery<TResponse>, PageResponse<TResponse>>
    where TEntity : class, IEntity
    where TResponse : IResponse
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetAllQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PageResponse<TResponse>> Handle(GetAllQuery<TResponse> query, CancellationToken cancellationToken)
        => new()
        {
            Data = await _context.Set<TEntity>()
                .AsNoTracking()
                .PaginateFrom(query.Page)
                .ProjectTo<TResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken),
            PageNumber = query.Page.PageNumber,
            PageSize = query.Page.PageSize,
            TotalItems = await _context.Countries.CountAsync(cancellationToken)
        };
}