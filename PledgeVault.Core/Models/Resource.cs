﻿using PledgeVault.Core.Contracts.Entities;
using PledgeVault.Core.Enums.Models;
using System;

namespace PledgeVault.Core.Models;

public sealed class Resource : IEntity
{
    public Resource()
    {
        EntityCreated = DateTime.Now;
        EntityActive = true;
    }

    public int Id { get; set; }

    public DateTime EntityCreated { get; set; }

    public DateTime? EntityModified { get; set; }

    public bool EntityActive { get; set; }

    public string Title { get; set; }

    public string SiteUrl { get; set; }

    public ResourceType ResourceType { get; set; }

    public string Summary { get; set; }

    public int PledgeId { get; init; }

    public Pledge Pledge { get; init; }
}