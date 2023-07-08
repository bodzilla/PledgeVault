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

namespace PledgeVault.Services.Handlers.Comments;

public sealed class GetByCommentIdQueryHandler : IRequestHandler<GetByIdQuery<CommentResponse>, CommentResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByCommentIdQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CommentResponse> Handle(GetByIdQuery<CommentResponse> query, CancellationToken cancellationToken)
    {
        if (query.Id <= 0) throw new InvalidRequestException();

        return await _context.Comments
            .AsNoTracking()
            .WithOnlyActiveEntities()
            .Where(x => x.Id == query.Id)
            .ProjectTo<CommentResponse>(_mapper.ConfigurationProvider, cancellationToken,
                x => x.User, x => x.Pledge)
            .SingleOrDefaultAsync(cancellationToken);
    }
}