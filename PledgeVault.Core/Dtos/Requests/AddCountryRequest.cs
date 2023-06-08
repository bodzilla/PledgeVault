using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Enums;
using System;

namespace PledgeVault.Core.Dtos.Requests;

public sealed record AddCountryRequest : IRequest
{
    public string Name { get; init; }

    public DateTime? DateEstablished { get; init; }

    public GovernmentType GovernmentType { get; init; }

    public string Summary { get; init; }
}