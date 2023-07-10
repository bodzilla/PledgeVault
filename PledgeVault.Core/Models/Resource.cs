using PledgeVault.Core.Contracts.Entities;
using PledgeVault.Core.Enums.Models;
using System;

namespace PledgeVault.Core.Models;

public sealed class Resource : IEntity
{
    public Resource()
    {
        EntityCreated = DateTime.Now;
        IsEntityActive = true;
    }

    public int Id { get; init; }

    public DateTime EntityCreated { get; init; }

    public DateTime? EntityModified { get; set; }

    public bool IsEntityActive { get; set; }

    public string Title { get; set; }

    public string SiteUrl { get; set; }

    public ResourceType ResourceType { get; set; }

    public string Summary { get; set; }

    public int UserId { get; init; }

    public User User { get; init; }

    public int PledgeId { get; init; }

    public Pledge Pledge { get; init; }
}