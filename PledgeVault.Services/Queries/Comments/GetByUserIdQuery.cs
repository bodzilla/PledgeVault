using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Comments;

public sealed record GetByUserIdQuery : IRequest<Page<CommentResponse>>
{
    public int Id { get; init; }

    public PageOptions PageOptions { get; init; }
}