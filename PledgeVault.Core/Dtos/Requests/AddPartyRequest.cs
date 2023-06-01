using System;
using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Core.Dtos.Requests;

public sealed class AddPartyRequest : IRequest
{
    public string Name { get; set; }

    public DateTime? DateEstablished { get; set; }

    public int CountryId { get; set; }

    public string LogoUrl { get; set; }

    public string SiteUrl { get; set; }

    public string Summary { get; set; }
}