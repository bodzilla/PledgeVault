﻿using System.Collections.Generic;
using System;
using PledgeVault.Core.Contracts;
using PledgeVault.Core.Enums;

namespace PledgeVault.Core.Dtos.Responses;

public sealed class PoliticianResponse : IResponse
{
    public PoliticianResponse() => Pledges = new List<PledgeResponse>();

    public int Id { get; set; }

    public string Name { get; set; }

    public SexType SexType { get; set; }

    public DateTime DateOfBirth { get; set; }

    public DateTime? DateOfDeath { get; set; }

    public string CountryOfBirth { get; set; }

    public PartyResponse Party { get; set; }

    public PositionResponse Position { get; set; }

    public bool IsPartyLeader { get; set; }

    public string PhotoUrl { get; set; }

    public string Summary { get; set; }

    public ICollection<PledgeResponse> Pledges { get; set; }
}