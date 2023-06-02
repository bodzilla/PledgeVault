using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Enums;

namespace PledgeVault.Core.Dtos.Requests;

public sealed class AddResourceRequest : IRequest
{
    public string Title { get; set; }

    public string SiteUrl { get; set; }

    public ResourceType ResourceType { get; set; }

    public string Summary { get; set; }

    public int PledgeId { get; set; }
}