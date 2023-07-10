using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Core.Dtos.Requests;

public sealed record UpdateCommentRequest : IRequest
{
    public int Id { get; init; }

    public int UserId { get; init; }

    public int PledgeId { get; init; }

    public string Text { get; init; }
}