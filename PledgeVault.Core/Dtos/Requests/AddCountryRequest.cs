using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Enums;
using System;

namespace PledgeVault.Core.Dtos.Requests;

public sealed class AddCountryRequest : IRequest
{
    public string Name { get; set; }

    public DateTime? DateEstablished { get; set; }

    public GovernmentType GovernmentType { get; set; }

    public string Summary { get; set; }
}