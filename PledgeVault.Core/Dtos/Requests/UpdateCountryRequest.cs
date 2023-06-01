using PledgeVault.Core.Enums;
using System;
using PledgeVault.Core.Contracts;

namespace PledgeVault.Core.Dtos.Requests;

public sealed class UpdateCountryRequest : IRequest
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime? DateEstablished { get; set; }

    public GovernmentType GovernmentType { get; set; }

    public string Summary { get; set; }
}