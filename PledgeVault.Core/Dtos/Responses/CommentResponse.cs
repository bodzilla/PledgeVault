using PledgeVault.Core.Contracts.Dtos;
using System;

namespace PledgeVault.Core.Dtos.Responses;

public sealed record CommentResponse : IResponse
{
    public int Id { get; init; }

    public DateTime EntityCreated { get; init; }

    public DateTime? EntityModified { get; init; }

    public int UserId { get; init; }

    public UserResponse User { get; init; }

    public int PledgeId { get; init; }

    public PledgeResponse Pledge { get; init; }

    public string Text { get; init; }
}