using System.Collections.Generic;
using PledgeVault.Core.Base;

namespace PledgeVault.Core.Models;

public sealed class Position : EntityBase
{
    public string Title { get; set; }

    public string Summary { get; set; }

    public ICollection<Politician> Politicians { get; set; }
}