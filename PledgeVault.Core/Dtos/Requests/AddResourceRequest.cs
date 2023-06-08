﻿using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Enums;

namespace PledgeVault.Core.Dtos.Requests;

public sealed record AddResourceRequest : IRequest
{
    public string Title { get; init; }

    public string SiteUrl { get; init; }

    public ResourceType ResourceType { get; init; }

    public string Summary { get; init; }

    public int PledgeId { get; init; }
}