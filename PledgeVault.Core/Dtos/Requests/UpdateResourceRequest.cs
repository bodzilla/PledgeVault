using PledgeVault.Core.Enums;
using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Core.Dtos.Requests;

public sealed class UpdateResourceRequest : IRequest
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string SiteUrl { get; set; }

    public ResourceType ResourceType { get; set; }

    public string Summary { get; set; }

    public int PledgeId { get; set; }
}