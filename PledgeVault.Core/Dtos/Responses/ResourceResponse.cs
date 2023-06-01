using PledgeVault.Core.Contracts;
using PledgeVault.Core.Enums;

namespace PledgeVault.Core.Dtos.Responses;

public sealed class ResourceResponse : IResponse
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string SiteUrl { get; set; }

    public ResourceType ResourceType { get; set; }

    public string Summary { get; set; }

    public PledgeResponse Pledge { get; set; }
}