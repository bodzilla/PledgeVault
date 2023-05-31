using PledgeVault.Core.Base;
using PledgeVault.Core.Enums;

namespace PledgeVault.Core.Models;

public sealed class Resource : EntityBase
{
    public string Title { get; set; }

    public string SiteUrl { get; set; }

    public ResourceType ResourceType { get; set; }

    public string Summary { get; set; }

    public int PledgeId { get; set; }

    public Pledge Pledge { get; set; }
}