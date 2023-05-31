using System;
using System.Collections.Generic;
using PledgeVault.Core.Base;
using PledgeVault.Core.Enums;

namespace PledgeVault.Core.Models;

public sealed class Country : EntityBase
{
    public Country() => Parties = new List<Party>();

    public string Name { get; set; }

    public DateTime? DateEstablished { get; set; }

    public GovernmentType GovernmentType { get; set; }

    public string Summary { get; set; }

    public ICollection<Party> Parties { get; set; }
}