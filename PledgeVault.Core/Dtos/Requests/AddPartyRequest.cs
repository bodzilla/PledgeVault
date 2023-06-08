using PledgeVault.Core.Contracts.Dtos;
using System;

namespace PledgeVault.Core.Dtos.Requests;

public sealed record AddPartyRequest : IRequest
{
    public string Name { get; init; }

    public DateTime? DateEstablished { get; init; }

    public int CountryId { get; init; }

    public string LogoUrl { get; init; }

    public string SiteUrl { get; init; }

    public string Summary { get; init; }
}