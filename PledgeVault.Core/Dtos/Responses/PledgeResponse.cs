using System.Collections.Generic;
using System;
using PledgeVault.Core.Contracts;
using PledgeVault.Core.Enums;

namespace PledgeVault.Core.Dtos.Responses;

public sealed class PledgeResponse : IResponse
{
    public PledgeResponse() => Resources = new List<ResourceResponse>();

    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime DatePledged { get; set; }

    public DateTime? DateFulfilled { get; set; }

    public PledgeCategoryType PledgeCategoryType { get; set; }

    public PledgeStatusType PledgeStatusType { get; set; }

    public PoliticianResponse Politician { get; set; }

    public string Summary { get; set; }

    public string FulfilledSummary { get; set; }

    public int Score { get; set; }

    public ICollection<ResourceResponse> Resources { get; set; }
}