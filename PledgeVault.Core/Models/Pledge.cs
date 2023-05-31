using PledgeVault.Core.Base;
using System;
using System.Collections.Generic;
using PledgeVault.Core.Enums;

namespace PledgeVault.Core.Models;

public sealed class Pledge : EntityBase
{
    public Pledge()
    {
        PledgeStatusType = PledgeStatusType.Pending;
        Resources = new List<Resource>();
    }

    public string Title { get; set; }

    public DateTime DatePledged { get; set; }

    public DateTime? DateFulfilled { get; set; }

    public PledgeCategoryType PledgeCategoryType { get; set; }

    public PledgeStatusType PledgeStatusType { get; set; }

    public int PoliticianId { get; set; }

    public Politician Politician { get; set; }

    public string Summary { get; set; }

    public string FulfilledSummary { get; set; }

    public int Score { get; set; }

    public ICollection<Resource> Resources { get; set; }
}