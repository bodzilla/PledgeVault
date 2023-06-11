using PledgeVault.Core.Contracts.Entities;
using PledgeVault.Core.Enums.Models;
using System;
using System.Collections.Generic;

namespace PledgeVault.Core.Models;

public sealed class Pledge : IEntity
{
    public Pledge()
    {
        EntityCreated = DateTime.Now;
        EntityActive = true;
        Resources = new List<Resource>();
    }

    public int Id { get; set; }

    public DateTime EntityCreated { get; set; }

    public DateTime? EntityModified { get; set; }

    public bool EntityActive { get; set; }

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