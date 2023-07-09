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
        IsEntityActive = true;
        Comments = new List<Comment>();
        Resources = new List<Resource>();
    }

    public int Id { get; init; }

    public DateTime EntityCreated { get; init; }

    public DateTime? EntityModified { get; set; }

    public bool IsEntityActive { get; set; }

    public string Title { get; set; }

    public DateTime DatePledged { get; set; }

    public DateTime? DateFulfilled { get; set; }

    public PledgeCategoryType PledgeCategoryType { get; set; }

    public PledgeStatusType PledgeStatusType { get; set; }

    public int UserId { get; init; }

    public User User { get; init; }

    public int PoliticianId { get; init; }

    public Politician Politician { get; init; }

    public int Score { get; set; }

    public string Summary { get; set; }

    public string FulfilledSummary { get; set; }

    public ICollection<Comment> Comments { get; init; }

    public ICollection<Resource> Resources { get; init; }
}