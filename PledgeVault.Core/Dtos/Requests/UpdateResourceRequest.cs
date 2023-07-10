using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Enums.Models;

namespace PledgeVault.Core.Dtos.Requests;

public sealed record UpdateResourceRequest : IRequest
{
    public int Id { get; init; }

    public string Title { get; init; }

    public string SiteUrl { get; init; }

    public ResourceType ResourceType { get; init; }

    public string Summary { get; init; }

    public int UserId { get; init; }

    public int PledgeId { get; init; }
}