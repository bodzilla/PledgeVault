using PledgeVault.Core.Contracts.Dtos;
using System;
using System.Collections.Generic;

namespace PledgeVault.Core.Dtos.Responses;

public sealed record UserResponse : IResponse
{
    public int Id { get; init; }

    public DateTime EntityCreated { get; init; }

    public string Username { get; init; }

    public IReadOnlyCollection<PledgeResponse> Pledges { get; init; }

    public IReadOnlyCollection<CommentResponse> Comments { get; init; }

    public IReadOnlyCollection<ResourceResponse> Resources { get; init; }
}