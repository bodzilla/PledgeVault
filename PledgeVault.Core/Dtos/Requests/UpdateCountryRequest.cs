using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Enums.Models;
using System;

namespace PledgeVault.Core.Dtos.Requests;

public sealed record UpdateCountryRequest : IRequest
{
    public int Id { get; init; }

    public string Name { get; init; }

    public DateTime? DateEstablished { get; init; }

    public GovernmentType GovernmentType { get; init; }

    public string Summary { get; init; }
}