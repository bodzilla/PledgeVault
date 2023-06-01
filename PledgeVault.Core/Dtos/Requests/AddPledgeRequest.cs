using PledgeVault.Core.Enums;
using System;
using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Core.Dtos.Requests;

public sealed class AddPledgeRequest : IRequest
{
    public AddPledgeRequest() => PledgeStatusType = PledgeStatusType.Pending;

    public string Title { get; set; }

    public DateTime DatePledged { get; set; }

    public DateTime? DateFulfilled { get; set; }

    public PledgeCategoryType PledgeCategoryType { get; set; }

    public PledgeStatusType PledgeStatusType { get; set; }

    public int PoliticianId { get; set; }

    public string Summary { get; set; }

    public string FulfilledSummary { get; set; }

    public int Score { get; set; }
}