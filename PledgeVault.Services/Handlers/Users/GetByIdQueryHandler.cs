using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using PledgeVault.Services.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Users;

internal sealed class GetByIdQueryHandler : IRequestHandler<GetByIdQuery<UserResponse>, UserResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByIdQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserResponse> Handle(GetByIdQuery<UserResponse> query, CancellationToken cancellationToken)
    {
        if (query.Id <= 0) throw new InvalidRequestException();

        return await _context.Users
            .AsNoTracking()
            .WithOnlyActiveEntities()
            .Where(x => x.Id == query.Id)
            .ProjectTo<UserResponse>(_mapper.ConfigurationProvider, cancellationToken,
                x => x.Pledges, x => x.Comments, x => x.Resources)
            .SingleOrDefaultAsync(cancellationToken);
    }
}