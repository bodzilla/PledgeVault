using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Enums.Models;
using System;

namespace PledgeVault.Core.Dtos.Responses;

public sealed record ResourceResponse : IResponse
{
    public int Id { get; init; }

    public DateTime EntityCreated { get; init; }

    public DateTime? EntityModified { get; init; }

    public string Title { get; init; }

    public string SiteUrl { get; init; }

    public ResourceType ResourceType { get; init; }

    public string Summary { get; init; }

    public int UserId { get; init; }

    public UserResponse User { get; init; }

    public int PledgeId { get; init; }

    public PledgeResponse Pledge { get; init; }
}